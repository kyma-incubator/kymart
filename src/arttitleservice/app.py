from flask import Flask, request, jsonify
from time import strftime
from aitextgen import aitextgen

server = Flask(__name__)
ai = aitextgen(model="./model/pytorch_model.bin", config="./model/config.json", to_gpu=False)

@server.route('/healthcheck', methods=['GET'])
def healthcheck():
    return 'Ok' 

@server.route('/title/<int:seed>', methods=['GET'])
def title(seed):
    # server.logger.warning('A warning occurred (%d apples)', test)
    # server.logger.error('An error occurred')
    # server.logger.info('Info')
    title=ai.generate_one(seed=seed, max_length=12)
    return jsonify({'title': title.rstrip()})    

@server.after_request
def after_request(response):
    timestamp = strftime('[%Y-%b-%d %H:%M]')
    server.logger.info('%s %s %s %s %s %s', timestamp, request.remote_addr, request.method, request.scheme, request.full_path, response.status)
    header = response.headers
    header['Access-Control-Allow-Origin'] = '*'
    return response
    