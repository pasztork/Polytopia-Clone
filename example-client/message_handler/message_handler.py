import sys

# Value set by app.py module
NAME = None

this = sys.modules[__name__]
this.tiles = None


def start_game(message):
    this.tiles = message['tiles']
    return {}


def start_turn(message):
    if (message['name'] != this.NAME):
        return {}
    return {'action': 'finishTurn'}


MESSAGE_FUNCTION_DICT = {
    'startGame': start_game,
    'startTurn': start_turn
}


def get_response_for(message):
    return MESSAGE_FUNCTION_DICT[message['action']](message)
