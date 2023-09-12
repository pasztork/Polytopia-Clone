import json
import sys
import random

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
        'Action': 'GetActions'
    }

def choose_action(message):
    coordinates = message['Buildings'][0]['Position']
    return {
        'Name': this.NAME,
        'Action': 'Train',
        'Parameters': {
            'Start': [coordinates[0], coordinates[1]],
            'Troop': 'Warrior'
        }
    }

def end_turn(message):
    return {
        'Name': this.NAME,
        'Action': 'EndTurn'
    }


MESSAGE_FUNCTION_DICT = {
    'Setup': setup,
    'StartTurn': start_turn,
    'ActionState': choose_action,
    'Response': end_turn,
}


def get_response_for(message):
    jsonmsg = json.loads(message)
    return MESSAGE_FUNCTION_DICT[jsonmsg['Type']](jsonmsg)
