# Proyecto de aula — Metodología de trabajo (SDD, DISEÑO, versiones, Git y secretos)

> **Léame primero.** Este documento define CÓMO se trabaja el proyecto de
> aula del curso **Diseño de Software (USB Medellín)** — la misma
> metodología del ejemplo que construimos en clase. Lo QUE construye cada
> equipo lo define el documento de su módulo (lo entrega el profesor en
> clase).

---

## 1. El método: SDD por versiones (igual que en clase)

El proyecto de aula se trabaja con **Spec-Driven Development (SDD)**:
primero la especificación, después el código, **versión por versión** —
exactamente como el ejemplo del curso:

| Ejemplo de clase | Qué demuestra |
|---|---|
| [proyecto_diseno_de_software1](https://github.com/ccastro2050/proyecto_diseno_de_software1) | La v1: una rebanada vertical con capas, especificada Y DIBUJADA antes de codificar (vea sus diagramas Mermaid) |
| proyecto_diseno_de_software2, 3… | Llegarán durante el semestre — la ruta continúa igual |

Lo que se replica del ejemplo **es el MÉTODO, no el contenido**: la
constitución permanente, una carpeta de specs por versión con sus
documentos, los criterios de aceptación como definición de "terminado",
el cierre con tag, la regla de que una versión cerrada no se reabre —
**y, en ESTE curso, el diseño dibujado dentro de la spec (sección 4)**.

### 1.1 Las reglas de oro

1. **La especificación manda**: no se programa nada que la spec de la
   versión en curso no pida.
2. **No se anticipa**: nada de una versión futura se construye "de una
   vez" (YAGNI con dirección: las interfaces sí, la fábrica cuando
   llegue el segundo motor).
3. **Una versión está TERMINADA** solo cuando pasan sus criterios de
   aceptación → commit + **tag `vN`** en main → solo entonces se escribe
   la spec de la siguiente.
4. **Una versión cerrada no se reabre**: los ajustes van en la siguiente.
5. **Regresión obligatoria**: al cerrar la vN, los criterios de TODAS las
   versiones anteriores deben seguir pasando.

## 2. Las 4 versiones del proyecto de aula

| Versión | Qué agrega (acumulativo) | Cierre |
|---|---|---|
| **v1** | CRUD de las **tablas sin FK** del módulo — API REST + Frontend funcionando | Criterios en verde + tag `v1` |
| **v2** | CRUD de **TODAS las tablas** (FK con listas desplegables cargadas desde la API; tablas puente) | Regresión v1 + criterios + tag `v2` |
| **v3** | **JWT + sesiones + control de acceso por roles** + CRUD de usuario/rol/rol_usuario (solo admin) | Regresión v1-v2 + criterios + tag `v3` |
| **v4** | Aplicativo completo: **10 consultas multitabla** (4+ tablas c/u), **dashboard**, **imagen corporativa**, responsive/PWA y **publicación** en servidor gratuito | Regresión total + criterios + tag `v4` |

### 2.1 Calendario y evaluación del semestre (100%)

La **fecha exacta** de su grupo la fija el profesor en clase (anótela).

| Momento | Fecha general | Fecha exacta (su grupo) | Evaluación |
|---|---|---|---|
| **Evaluación individual teórico-práctica** | Segunda semana de **septiembre** | \_\_\_\_/\_\_\_\_/\_\_\_\_\_\_\_\_ | **20%** individual |
| **Entrega versión 1** | Penúltima semana de **septiembre** | \_\_\_\_/\_\_\_\_/\_\_\_\_\_\_\_\_ | **20%** — 10% sustentación individual (incluidos los commits) + 10% equipo |
| **Entrega versión 2** | Penúltima semana de **octubre** | \_\_\_\_/\_\_\_\_/\_\_\_\_\_\_\_\_ | **20%** — 10% + 10% |
| **Entrega versión 3** | Primera semana de **noviembre** | \_\_\_\_/\_\_\_\_/\_\_\_\_\_\_\_\_ | **20%** — 10% + 10% |
| **Entrega versión 4** | Penúltima semana de **noviembre** | \_\_\_\_/\_\_\_\_/\_\_\_\_\_\_\_\_ | **20%** — 10% + 10% |

> **"Incluidos los commits"**: en la sustentación individual cada
> estudiante responde por SU rama, y sus commits lo respaldan. Una rama
> con un solo commit gigante la noche anterior es una sustentación sin
> evidencia.

## 3. El spec kit que cada equipo ESCRIBE (por versión)

Antes de programar cada versión, el equipo escribe su especificación en
`docs/spec_kit/` del repositorio de la API (el mismo formato del curso):

```
docs/spec_kit/
├── 1_constitution.md            ← UNA vez (los principios del equipo: stack
│                                   elegido, capas, español, borrado lógico,
│                                   secretos por variables de entorno…)
└── versiones/
    ├── 0_mapa_versiones.md      ← la tabla de la sección 2, con estados
    ├── v1_<nombre>/             ← 2_spec.md · 3_plan.md · 4_research.md ·
    │                              5_data_model.md · 6_contracts.md ·
    │                              7_quickstart.md · 8_tasks.md ·
    │                              HISTORIAS_DE_USUARIO.md · GUIA_IA1.md*
    ├── v2_<nombre>/             ← los mismos, para el delta de la v2
    └── …
```

- **`HISTORIAS_DE_USUARIO.md` es obligatoria en este curso**: las
  tarjetas con el formato de clase (número, usuario, prioridad, riesgo,
  puntos, descripción "Yo… como… quiero… para…", observaciones con
  trazabilidad y **criterios de aceptación numerados**). El ejemplo:
  [las historias de la v1 del curso](https://github.com/ccastro2050/proyecto_diseno_de_software1/blob/main/docs/spec_kit/versiones/v1_producto_postgres/HISTORIAS_DE_USUARIO.md).
- \* La `GUIA_IA<N>.md` es opcional pero recomendada: si construyen con
  IA, escriban el prompt y las reglas como en las guías del curso.

**La spec es parte de la nota**: se evalúa que exista, esté completa,
**coincida con lo construido** — y que traiga los diagramas de la
sección 4.

## 4. EL SELLO DE ESTE CURSO: los diagramas Mermaid obligatorios

Esto es Diseño de Software: **el diseño se DIBUJA, y se dibuja DENTRO de
los `.md` del spec kit** — como bloques ```` ```mermaid ```` (texto que
GitHub renderiza y que la IA lee como parte de la especificación; jamás
imágenes pegadas). El ejemplo vivo es el spec kit de
[proyecto_diseno_de_software1](https://github.com/ccastro2050/proyecto_diseno_de_software1/tree/main/docs/spec_kit):
cada diagrama exigido aquí existe allá, con su guía de lectura.

**Qué diagrama va en qué documento (obligatorios por versión):**

| Documento | Diagrama(s) Mermaid obligatorio(s) | Tipo Mermaid | Qué debe mostrar |
|---|---|---|---|
| `1_constitution.md` | **La regla de dependencias** (una sola vez, no por versión) | `flowchart` | Las capas del equipo y las ÚNICAS flechas permitidas (cruzando por interfaces/contratos) |
| `2_spec.md` | **Diagrama de contexto** | `flowchart` | El sistema de ESTA versión, sus vecinos (usuarios, front, BD, servicios externos) y qué viaja por cada flecha |
| `3_plan.md` | **Despliegue** + **clases** + **secuencia del camino feliz** | `flowchart` · `classDiagram` · `sequenceDiagram` | Los contenedores/servicios del compose con sus puertos · la rebanada principal con sus interfaces · UNA operación completa viajando por las capas |
| `5_data_model.md` | **Entidad-relación** | `erDiagram` | Las tablas de la versión con PK/FK/cardinalidades (y qué columnas escribe la BD, no la API) |
| `6_contracts.md` | **Las secuencias de ERROR** | `sequenceDiagram` | Mínimo dos: el 404 (quién decide "no existe") y el 422/400 (dónde corta la validación) — con `Note` explicando qué capa aporta qué |
| `7_quickstart.md` | **El ciclo de validación** | `flowchart` | Arrancar → probar criterios → verde = tag / rojo = corregir |
| `8_tasks.md` | **El orden de fases con compuertas** | `flowchart` | Cada fase con su "Verificar:" como condición de la flecha |
| `4_research.md` | (sin diagrama obligatorio) | — | Es el registro de ADRs: texto con opciones, criterios y consecuencias; una tabla comparativa vale más que un dibujo aquí |

**Las tres reglas de los diagramas:**

1. **Embebidos y en texto**: bloques ```` ```mermaid ```` dentro del
   `.md`. Un PNG pegado NO cumple (no se versiona con diff ni lo lee la
   IA cuando le entreguen la spec).
2. **Cada diagrama con su guía de lectura**: 1-3 frases debajo diciendo
   cómo leerlo (cajas = quién, flechas = qué). Si el diagrama necesita
   media página de explicación, simplifique el diagrama.
3. **El diagrama es contrato**: si la secuencia dice que el servicio
   lanza la excepción y el controller la traduce, el código debe hacer
   EXACTAMENTE eso. Diagrama y código en desacuerdo = criterio de diseño
   no cumplido.

> En la sustentación individual se puede pedir: "léame su diagrama de
> secuencia del 404" o "¿por qué esta flecha no puede existir en su
> diagrama de dependencias?" — los diagramas son de TODO el equipo.

## 5. Los dos repositorios (y las reglas de GitHub)

| Repositorio | Qué es | Regla de oro |
|---|---|---|
| `<equipo>-api` | El backend: REST + JSON, conecta a la BD | NO genera HTML |
| `<equipo>-frontend` | La interfaz: consume la API por HTTP | NO se conecta a la BD |

**Requisitos obligatorios de ambos repositorios:**

1. **Privados.** El código del equipo no es público.
2. **Invitar al profesor** como colaborador desde el primer día:
   *Settings → Collaborators → Add people* → **`ccastro2050`**.
   Sin acceso del profesor, la entrega no existe.
3. El spec kit (con sus diagramas) vive en el repo de la API.

### 5.1 El flujo de ramas (obligatorio desde la v1)

- **NADIE trabaja en `main`. Nunca.** Ni un commit directo.
- **Cada estudiante tiene SU rama** (`rama-mariana`, `rama-jorge`) y
  trabaja siempre ahí.
- El equipo designa **UN encargado del main** (el integrador), que
  también tiene su propia rama — su rol extra es ser el único que hace
  merge.
- Todo llega a `main` por **Pull Request** revisado por el encargado
  (¿compila? ¿cumple la spec? ¿los criterios siguen pasando?).
- El cierre de cada versión es un **tag `vN` sobre main**.
- Commits pequeños, frecuentes y descriptivos en español — "avances" no
  es un mensaje.

```
rama-mariana ──●──●──●──╮ PR
rama-jorge   ──●──●─────┤ PR      (revisa y hace merge: SOLO el encargado)
rama-andres  ──●──●──●──┤ PR
                        ▼
main         ────────●──●──●── tag v1 ──●──●── tag v2 ──…
```

## 6. Secretos: variables de entorno, SIEMPRE

**Regla innegociable:** ningún secreto va escrito en el código ni en
archivos versionados (cadena de conexión, secreto del JWT, claves de
servicios externos).

> **Aclaración del profesor:** en los repositorios del curso las
> credenciales están a la vista **a propósito y solo por didáctica** (es
> un laboratorio que corre en su PC). El proyecto de aula se publica en
> un servidor real (v4): ahí la regla aplica completa desde la v1.

1. El código lee los secretos de **variables de entorno** (`.env` local,
   `environment:` en compose, panel del servidor en la v4).
2. **`.env` NUNCA se sube** — al `.gitignore` desde el primer commit.
3. El repo SÍ incluye **`.env.example`** con valores de mentira.
4. **Si un secreto se subió por error**: rotarlo de inmediato — borrarlo
   del último commit no basta, quedó en la historia.

En la rúbrica: un secreto quemado **anula el criterio de seguridad de la
versión**.

## 7. Reglas técnicas del sistema (aplican a todos los módulos)

- **API REST**: JSON siempre; códigos HTTP correctos (200/201, 400, 401,
  403, 404, 422, 500); y **un juego de endpoints ESPECÍFICO por cada tabla**:

  ```
  GET /api/sede          GET /api/sede/{id}          POST /api/sede
  PUT /api/sede/{id}     PATCH /api/sede/{id}        DELETE /api/sede/{id}

  GET /api/modalidad     GET /api/modalidad/{id}     POST /api/modalidad
  ...  y así con cada tabla del módulo
  ```

  El borrado es **lógico** (`activo = 0` / `activo = FALSE`) y los listados
  filtran los inactivos.

### 6.1 Por qué el proyecto pide endpoints específicos y no una API genérica

Al ver ocho o diez tablas parecidas, la idea aparece sola: **una sola ruta con
el nombre de la tabla como parámetro** —`GET /api/{tabla}`, `POST
/api/{recurso}`— atendida por un controlador, un servicio y un repositorio
únicos que sirven para todo.

Es una buena idea en su lugar, y **en este proyecto no es el lugar**. Vale la
pena explicar por qué, porque el argumento sirve mucho más allá del curso.

#### La distinción que hay que entender: prototipo contra producción

Una API genérica **sirve para un prototipo**. Es más corta, se escribe en una
tarde y demuestra que la idea funciona. El problema no es que esté mal escrita:
es que **un prototipo y un sistema en producción se optimizan para cosas
distintas**.

| | Un prototipo | Producción |
|---|---|---|
| **¿Cuántas veces se escribe?** | Una | Una |
| **¿Cuántas veces se LEE y se cambia?** | Casi ninguna: se tira | Durante años, y casi siempre por alguien que no lo escribió |
| **¿Quién lo consume?** | Quien lo escribió, ese mismo día | Otro equipo, otro sistema, y usted mismo seis meses después |

Genérico es **barato de escribir y caro de vivir**. Específico es **caro de
escribir una vez y barato de vivir**. En un prototipo gana el primero porque
no hay «vivir». En producción gana el segundo, y por goleada.

De ahí salen seis consecuencias concretas:

| # | Lo que pasa en producción | Por qué el molde genérico no aguanta |
|---|---|---|
| 1 | **El contrato hay que LEERLO, no recordarlo** | Swagger muestra `/api/{tabla}`: un hueco. Quien abre la API no sabe qué recursos hay ni qué campos lleva cada uno. Y este proyecto se sustenta proyectando Swagger: **si no dice qué hay, no hay qué sustentar** |
| 2 | **Las tablas se van diferenciando** | Toda tabla que sobrevive acumula reglas propias: una no se borra, otra audita, otra tiene un campo calculado. El molde obliga a meter condiciones «si la tabla es X…», y ahí el ahorro se acabó: queda un intérprete escrito a mano, peor que los diez controladores que se querían evitar |
| 3 | **Los permisos son POR RECURSO** | «El coordinador ve sedes pero no roles» no cabe en una ruta única: o se autoriza de más, o se agrega un mapa de reglas por tabla — que es la lista de controladores otra vez, hecha a mano y sin ayuda del compilador |
| 4 | **Hay que operar el sistema** | Métricas, registros, límites de tasa, alertas: todo se agrupa por ruta. Con `/api/{tabla}` todo el tráfico es **una sola línea** en el tablero, y nunca se puede decir «el endpoint de sedes está lento» |
| 5 | **Hay que cambiar un recurso sin tocar los demás** | Agregar un campo, cambiar una validación, deprecar algo. Con un molde compartido, **cada cambio pequeño toca las diez tablas**: el riesgo se multiplica por diez |
| 6 | **Los errores en ejecución cuestan incidentes** | Con clases de petición tipadas, un campo mal escrito no compila. Con un diccionario genérico, se descubre en la sustentación o, peor, en producción |

#### Y la excepción honesta, para que esto no sea dogma

**Sí existen APIs genéricas en producción**, y buenas: PostgREST, Hasura, los
paneles de administración que exponen tablas. La diferencia es que en esos
casos **lo genérico es el producto entero**: publican un esquema completo
—generado de la base—, tienen su modelo de permisos por fila y por columna, y
su contrato *es* «esto expone la base de datos».

Lo que no funciona es **la mitad**: una API de dominio, escrita a mano, con un
pedazo genérico adentro. Ahí se pagan los costos de las dos formas y no se
cobra la ventaja de ninguna.

> **De dónde sale esto.** No es una preferencia de nadie: sale de haberlo
> hecho de las dos formas. El ejemplo de referencia del curso se construyó
> primero con una ruta genérica — quedó más corto, funcionaba y pasaba sus
> pruebas —, y al abrir Swagger para sustentarlo no se veía un solo recurso
> con nombre. Se rehízo con endpoints específicos.
>
> Se cuenta aquí para que usted no tenga que pagar la misma tarde.
- **Separación estricta**: si el frontend toca la BD, la arquitectura
  está rota (criterio de rúbrica).
- **v3 — seguridad**: `POST /api/login` entrega el JWT; middleware de
  autenticación y autorización por roles; contraseñas **hasheadas**
  (bcrypt o equivalente), nunca en texto plano.
- **v4 — cierre**: 10 consultas multitabla (4+ tablas c/u) con
  dashboard; páginas corporativas con identidad; responsive/PWA;
  publicación en servidor gratuito aprobado por el profesor.
- **Stack**: cada equipo elige su lenguaje/framework con aprobación del
  profesor — la metodología, los contratos y LOS DIAGRAMAS son los
  mismos en cualquier stack (esa es la gracia).

## 8. Rúbrica de evaluación

Cada criterio se califica en una de dos franjas: **Cumple (3.0 – 5.0,
según calidad)** o **No cumple (0 – 2.9)**.

| Criterio | Cumple (3.0 – 5.0) | No cumple (0 – 2.9) |
|---|---|---|
| **Especificación (SDD)** | Spec kit completo ANTES del código; criterios verificables; coincide con lo construido | No hay spec, se escribió después, o contradice lo construido |
| **DISEÑO (diagramas del spec kit)** | Los diagramas obligatorios de la sección 4 presentes, en Mermaid embebido, con guía de lectura, y COINCIDEN con el código (la secuencia del 404 es la del código) | Diagramas faltantes, pegados como imagen, sin guía, o en desacuerdo con lo construido |
| **Historias de usuario** | Tarjetas completas con criterios de aceptación verificables y trazabilidad a la spec | Sin historias, sin criterios, o desconectadas de la spec |
| **Funcionalidad de la API** | Endpoints de la versión con JSON y códigos correctos | Endpoints caídos o sin JSON |
| **Funcionalidad del Frontend** | Consume la API y es usable | No funciona o va directo a la BD |
| **Separación API/Front** | El front jamás toca la BD | No hay separación |
| **Seguridad (v3+) y secretos (todas)** | JWT + roles; contraseñas hasheadas; cero secretos en el código; `.env.example` presente | Sin autenticación o secretos quemados |
| **Borrado lógico** | En las tablas de la versión, inactivos filtrados | Borrado físico |
| **Git y GitHub** | Repos privados con el profesor; cada uno en su rama; TODO por PR; solo el encargado hace merge; tags; commits descriptivos | Commits a main, repo público, o "un solo commit con todo" |
| **Dashboard y consultas (v4)** | 10 consultas de 4+ tablas con gráficos | Menos, o sin dashboard |
| **Publicación (v4)** | Publicado y funcional, secretos en el panel del servidor | No publicado o secretos expuestos |

**Entregar en cada versión:** enlaces a los 2 repos (con el tag `vN`
puesto) + evidencia del quickstart pasando. En la v4, además: URL del
sitio publicado.
