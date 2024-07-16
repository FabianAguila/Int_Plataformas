from flask import Flask, request
from flask_restful import Api, Resource
from flask_cors import CORS
import requests
app = Flask(__name__)
api = Api(app)
CORS(app, resources={r"/api/*": {"origins": "*"}})
#            host           / endpoint
url_Cliente = "http://localhost:5001/api/Cliente"
url_Producto = "http://localhost:5001/api/Productos"
url_Compras = "http://localhost:5010/api/Compras"


def obtener_producto(id):
    try:
        producto_response = requests.get(f"{url_Producto}/{id}")
        producto_response.raise_for_status()
        return producto_response.json()
    except requests.exceptions.RequestException as e:
        raise Exception(f"Error en obtener el ID del producto: {str(e)}")

def obtener_cli(id):
    try:
        cliente_response = requests.get(f"{url_Cliente}/{id}")
        cliente_response.raise_for_status()
        return cliente_response.json()
    except requests.exceptions.RequestException as e:
        raise Exception(f"Error en obtener el ID del cliente: {str(e)}")
    
def obtener_compras(id):
    try: 
        compras_response = requests.get(f"{url_Compras}/{id}")
        compras_response.raise_for_status()
        return compras_response.json()
    except requests.exceptions.RequestException as e:
        raise Exception(f"Error en obtener el ID del cliente: {str(e)}")
    
class Boleta(Resource):
    def post(self):
        try:
            data = request.get_json()

            # Validar datos de entrada
            if not data or 'clienteId' not in data or 'productoId' not in data:
                return {"error": "Datos incompletos"}, 400

            cliente_id = data['clienteId']
            producto_id = data['productoId']
            cantidad = data.get('cantidad', 1)
            total_costo = data.get('totalCosto', 0)

            # Verificar si el cliente existe
            try:
                cliente = obtener_cli(cliente_id)
            except Exception as e:
                return {"error": str(e)}, 404

            # Verificar si el producto existe
            try:
                producto = obtener_producto(producto_id)
            except Exception as e:
                return {"error": str(e)}, 404

            # Crear nueva compra
            nueva_compra = {
                "clienteId": cliente_id,
                "productoId": producto_id,
                "cantidad": cantidad,
                "totalCosto": total_costo,
                "cliente": {
                    "id": cliente_id,
                    "nombre": cliente['nombre'],
                    "direccion": cliente['direccion'],
                    "telefono": cliente['telefono'],
                    "email": cliente['email']
                },
                "producto": {
                    "id": producto_id,
                    "nombre": producto['nombre'],
                    "precio": producto['precio'],
                    "stock": producto['stock'],
                    "cantidad": cantidad
                }
            }

            # Enviar nueva compra a la API de Compras
            compra_response = requests.post(url_Compras, json=nueva_compra)
            compra_response.raise_for_status()

            return compra_response.json(), 201

        except Exception as e:
            return {"error": str(e)}, 500

# Registro de recursos
api.add_resource(Boleta, "/api/Compras", methods=['POST'])

# Iniciar la aplicación Flask
if __name__ == "__main__":
    app.run(port=5001, debug=True)