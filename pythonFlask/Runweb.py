import asyncio
from flask import Flask, request
from CVfuntion import *

app = Flask(__name__)

loop = asyncio.get_event_loop()

@app.route('/')
def index():
    return 'Hello world'

@app.route('/fun1',methods = ['POST'])
def web_fun1():
    path_v = request.form['video']

    try:
        #wait until fun1 finish
        s = loop.run_until_complete(fun1(path_v))
    except:
        return '{"resp":"busy","code","002"}'

    return '{"resp":"'+s+'","code","001"}'

@app.route('/fun2',methods = ['POST'])
def web_fun2():
    path_v = request.form['video']   
    return '{"resp":"'+path_v+'","code","001"}'

if __name__ == '__main__':
    app.run(debug=False, host='0.0.0.0',port=5001)