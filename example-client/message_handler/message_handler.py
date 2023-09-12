import json
import sys

# Value set by app.py module
NAME = None

this = sys.modules[__name__]
this.tiles = None


def setup(message):
    this.tiles = message['Tiles']
    return None


def start_turn(message):
    if (message['Name'] != this.NAME):
        return {}
    return {
        'Name': this.NAME,
        'Action': 'EndTurn'
    }


MESSAGE_FUNCTION_DICT = {
    'Setup': setup,
    'StartTurn': start_turn,
}


def get_response_for(message):
    jsonmsg = json.loads(message)
    return MESSAGE_FUNCTION_DICT[jsonmsg['Action']](jsonmsg)