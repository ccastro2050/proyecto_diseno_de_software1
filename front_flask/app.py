"""
app.py — El ensamblador del FRONT (Flask + Jinja2).

El front no tiene negocio ni base de datos: rutas que muestran HTML y un
cliente HTTP que habla con la API.

EN ESTA VERSION NO HAY SESION, y es a proposito: la puerta —identificarse, el
token, el 401 y el 403— es la VERSION 3. Hoy cualquiera que llegue a la
direccion entra, y eso es exactamente lo que la v3 arregla. Ponerlo antes
seria anticipar, y le quitaria a la v3 su razon de ser.
"""

import os

from flask import Flask, redirect, render_template, url_for

from entidades import ENTIDADES
from rutas_entidades import bp as bp_entidades

app = Flask(__name__)
app.secret_key = os.environ.get("CLAVE_SESION", "clave-solo-para-desarrollo")
app.register_blueprint(bp_entidades)


# Las tarjetas del inicio que NO salen del registro, porque no son
# un CRUD de campos: la factura se emite y se anula, el usuario con sus
# roles viaja con casillas, y el tablero no tiene tabla.
# En esta version la lista esta VACIA, y es correcto: la factura y los
# usuarios con sus roles llegan en la v2, y el tablero en la v4. Aqui todas
# las interfaces salen del registro.
TARJETAS_SUELTAS = []


@app.context_processor
def menu():
    """El menu, con TODAS las entidades de esta version.

    En la v3 este mismo metodo filtrara por permiso. Hoy no hay a quien
    preguntarle: no hay sesion.
    """
    return {"menu_entidades": ENTIDADES, "hay_sesion": False, "tarjetas_sueltas": TARJETAS_SUELTAS}


@app.route("/")
def inicio():
    return render_template("inicio.html", entidades=ENTIDADES)


if __name__ == "__main__":
    # debug=True recarga al guardar un .py. Es de DESARROLLO: en un servidor
    # real se usa un servidor WSGI (gunicorn, waitress), nunca este.
    app.run(host="0.0.0.0", port=int(os.environ.get("PUERTO", "8067")), debug=True)
