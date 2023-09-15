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
    return get_actions(message)

def get_actions(message):
    return {
        'Name': this.NAME,
        'Action': 'GetActions'
    }

def choose_action(message):
    troopsList = message['Troops']
    buildingsList = message['Buildings']
    techsList = message['TechsToUnlock']

    if(len(troopsList) > 0):
        troop = random.choice(troopsList)
        enemyTroops = troop['TroopsToAttack']
        enemyBuildings = troop['BuildingsToAttack']
        buildingsToBuild = troop['BuildingsToBuild']
        tilesToMove = troop['TilesToMove']
        if(len(enemyTroops) > 0):
            enemy = random.choice(enemyTroops)
            return {
                "Name": this.NAME,
                "Action": "AttackTroop",
                "Parameters": {
                    "Start": [troop['Position'][0], troop['Position'][1]],
                    "End": [enemy['Position'][0], enemy['Position'][1]]
                }
            }
        elif(len(enemyBuildings) > 0):
            enemy = random.choice(enemyBuildings)
            return {
                'Name': this.NAME,
                'Action': 'AttackBuilding',
                'Parameters': {
                    'Start': [troop['Position'][0], troop['Position'][1]],
                    'End': [enemy['Position'][0], enemy['Position'][1]]
                }
            }
        elif(len(buildingsToBuild) > 0):
            building = random.choice(buildingsToBuild)
            return {
                'Name': this.NAME,
                'Action': 'Build',
                'Parameters': {
                    'Start': [troop['Position'][0], troop['Position'][1]],
                    'Building': building['Type']
                }
            }
        else:
            tile = random.choice(tilesToMove)
            return {
                'Name': this.NAME,
                'Action': 'Move',
                'Parameters': {
                    'Start': [troop['Position'][0], troop['Position'][1]],
                    'End': [tile[0], tile[1]]
                }
            }
    elif(len(buildingsList) > 0):
        building = random.choice(buildingsList)
        troopsToTrain = building['TroopsToTrain']
        if(len(troopsToTrain) > 0):
            troop = random.choice(troopsToTrain)
            return {
                'Name': this.NAME,
                'Action': 'Train',
                'Parameters': {
                    'Start': [building['Position'][0], building['Position'][1]],
                    'Troop': troop['Type']
                }
            }
    elif(len(techsList) > 0):
        tech = random.choice(techsList)
        return {
            'Name': this.NAME,
            'Action': 'Learn',
            'Parameters': {
                "Tech": tech['Name']
            }
        }
    else:
        return {
            'Name': this.NAME,
            'Action': 'EndTurn'
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
    'Response': get_actions,
}


def get_response_for(message):
    jsonmsg = json.loads(message)
    if 'Type' in jsonmsg:
        return MESSAGE_FUNCTION_DICT[jsonmsg['Type']](jsonmsg)
