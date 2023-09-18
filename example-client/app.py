import json
import websocket

from message_handler import message_handler_v2

SERVER_URL = 'ws://localhost:53658/ws'
NAME = 'Attila'


def on_open(ws):
    message = { 'Name': NAME }
    ws.send(json.dumps(message))


def on_message(ws, message):
    response = message_handler_v2.get_response_for(message)
    if (response == None):
        return
    ws.send(json.dumps(response))


def connect_and_listen():
    websocket.enableTrace(True)
    ws = websocket.WebSocketApp(
        SERVER_URL, on_open=on_open, on_message=on_message)
    ws.run_forever()


if __name__ == '__main__':
    message_handler_v2.NAME = NAME
    connect_and_listen()
