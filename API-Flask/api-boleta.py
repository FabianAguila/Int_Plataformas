from flask import Flask, request
from flask_restful import Api, Resource
import requests
app = Flask(__name__)
api = Api(app)
#            host           / endpoint
url = "http://localhost:5000/api/cliente"
url_prod = "http://localhost:5000/api/producto"

class Boleta(Resource):
    def post(self):
        # capturar la información del body
        data = request.get_json()
        # envian: producto_id, cantidad, cliente_id

        # Obtener cliente
        cliente_response = requests.get(url+"/"+str(data["cliente_id"]))
        cliente = cliente_response.json()
    
api.add_resource(Boleta, "/api/boleta")

# POST localhost/api/boleta >> Boleta.post();

app.run(port=5001, debug=True)
