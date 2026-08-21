// ============================================================
// RepositorioProductoPostgres — la capa de DATOS de la v1.
//
// Única clase del sistema que habla SQL y que conoce la conexión.
// Cumple el contrato IRepositorioProducto.
//
// Usa DAPPER como micro-ejecutor A PROPÓSITO: el SQL sigue escrito
// A MANO, visible y SIEMPRE parametrizado — Dapper solo elimina el
// boilerplate del mapeo fila→objeto (columna "codigo" → propiedad
// Codigo, sin GetString(0) a mano) y lo hace con código cacheado,
// tan rápido como el mapeo manual. SIN Entity Framework: aquí nada
// genera SQL por nosotros (decisión D1 de 4_research.md).
// Reglas de la constitución que se cumplen aquí:
// - SQL SIEMPRE con parámetros @nombre (nunca concatenar valores).
// - Todo asíncrono (async/await).
// ============================================================

using ApiFacturas.Modelos;
using Dapper;    // el micro-ejecutor: Query/Execute sobre la conexión
using Npgsql;    // el proveedor oficial de PostgreSQL

namespace ApiFacturas.Repositorios;

public class RepositorioProductoPostgres : IRepositorioProducto
{
    // La cadena de conexión llega POR CONSTRUCTOR (este archivo no
    // sabe de appsettings ni de variables de entorno — eso es del
    // ensamblador). readonly = se asigna una vez.
    private readonly string _cadenaConexion;

    public RepositorioProductoPostgres(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    // ------------------------------------------------------------
    // Ayudante privado
    // ------------------------------------------------------------

    /// <summary>Crea la conexión (cerrada). Dapper la abre y la cierra
    /// solo en cada operación; el "await using" del que llama garantiza
    /// que se libere aunque haya error.</summary>
    private NpgsqlConnection CrearConexion() => new(_cadenaConexion);

    // ------------------------------------------------------------
    // Los 5 métodos del contrato
    // ------------------------------------------------------------

    public async Task<List<Producto>> ObtenerTodosAsync(int limite)
    {
        // El SQL con PARÁMETROS (@limite): el valor viaja por aparte y
        // el motor jamás lo confunde con SQL — eso evita la inyección.
        // "LIMIT @limite" es el Top-N del dialecto PostgreSQL (va al final).
        const string sql = @"SELECT codigo, nombre, stock, valorunitario
                             FROM producto ORDER BY codigo LIMIT @limite";

        await using var conexion = CrearConexion();
        // QueryAsync<Producto>: ejecuta el SQL y arma UN Producto por
        // fila, casando columna→propiedad por nombre (sin distinguir
        // mayúsculas). El objeto anónimo enlaza @limite ← limite:
        var filas = await conexion.QueryAsync<Producto>(sql, new { limite });
        return filas.ToList();
    }

    public async Task<Producto?> ObtenerPorCodigoAsync(string codigo)
    {
        const string sql = @"SELECT codigo, nombre, stock, valorunitario
                             FROM producto WHERE codigo = @codigo";

        await using var conexion = CrearConexion();
        // FirstOrDefault: una fila → el modelo; cero filas → null (el
        // contrato — el SERVICIO decide qué significa ese null):
        return await conexion.QueryFirstOrDefaultAsync<Producto>(sql, new { codigo });
    }

    public async Task CrearAsync(Producto producto)
    {
        const string sql = @"INSERT INTO producto (codigo, nombre, stock, valorunitario)
                             VALUES (@Codigo, @Nombre, @Stock, @Valorunitario)";

        await using var conexion = CrearConexion();
        // El OBJETO del modelo entero como fuente de parámetros:
        // cada propiedad alimenta el @parametro de su mismo nombre.
        await conexion.ExecuteAsync(sql, producto);
    }

    public async Task<int> ActualizarAsync(string codigo, Dictionary<string, object> datos)
    {
        // SET dinámico SOLO con las columnas que llegaron (PUT manda las
        // 3, PATCH un subconjunto). Los NOMBRES de columna vienen del
        // controlador, que los sacó de las PETICIONES (lista blanca) —
        // nunca del cliente — por eso es seguro armarlos en el texto;
        // los VALORES sí van siempre como parámetros.
        var asignaciones = new List<string>();
        foreach (var columna in datos.Keys)
        {
            asignaciones.Add($"{columna} = @{columna}");
        }
        // El parámetro de la clave se llama distinto (@codigo_clave) para
        // no chocar con un posible campo del SET:
        var sql = $"UPDATE producto SET {string.Join(", ", asignaciones)} " +
                  "WHERE codigo = @codigo_clave";

        // DynamicParameters: la bolsa de parámetros de Dapper — recibe el
        // diccionario tal cual y se le suma la clave:
        var parametros = new DynamicParameters(datos);
        parametros.Add("codigo_clave", codigo);

        await using var conexion = CrearConexion();
        // ExecuteAsync devuelve las FILAS AFECTADAS (0 = no existía).
        // Nota didáctica: PostgreSQL cuenta las filas que CUMPLIERON el
        // WHERE (aunque el valor nuevo sea igual al viejo) — por eso un
        // PATCH con el mismo valor reporta 1 fila, como debe ser.
        return await conexion.ExecuteAsync(sql, parametros);
    }

    public async Task<int> EliminarAsync(string codigo)
    {
        const string sql = "DELETE FROM producto WHERE codigo = @codigo";

        await using var conexion = CrearConexion();
        return await conexion.ExecuteAsync(sql, new { codigo });
    }
}
