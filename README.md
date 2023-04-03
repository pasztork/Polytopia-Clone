# Polytopia-Clone

Ez a projekt "Önálló laboratóriumra" készült. A "The Battle of Polytopia" c. játék egy másolata.

## Alapszabályok

Területek
 - víz
 - mező: sík mezőnek számít
 - erdő: sík mező
 - hegy: magaslati mezőnek számít
 - sivatag: sík mezőnek számít
 Egy mezőn csak egy egység tartózkodhat egy időben.

Fizetőeszközök
- pénz:  a város, valamint a területén lévő bankok és kikötők termelik
- nyersanyag:  a város, valamint a területén lévő nyersanyag termelők termelik
- élelem: a város, valamint a területén lévő farmok és kikötők termelik

Egységek
- felderítő: 3 egységet tud mozogni alapból
- harcos: 1 egységet tud mozogni alapból
- íjász: 2 egységet tud mozogni alapból
- hajó: 2 egységet tud alapból mozogni, alapból kikötőben jön létre
- telepes: 2 egység a mozgása, új várost tud létrehozni
- építész: épületeket tud építeni
- katapult: 9 mezőn sebez, a target mezőt duplán

Épületek
- város: van alaptermelése mindhárom alap nyersanyagból
- főváros: a kezdő város, nem külön épület típus
- nyersanyag termelő (bánya, fatelep): nyersanyagot termel körönként
- bank: pénzt termel körönként
- farm: élelmet termel körönként
- kikötő: vízi épület, pénzt és élelmet termel, hajókat lehet létrehozni benne

Technológiák, képességek
- archery: elérhetővé teszi az íjászt
- catapulting: elérhetővé teszi a katapult egységet, ami extra sebzést ad a városfalak ellen
- sailing: elérhetővé teszi a hajót

- riding: eggyel megnöveli a harcos egységek mozgását
- navigation: eggyel megnöveli a hajók mozgását
- strategy: növeli a támadás elhárításának valószínűségét
- militarism: növeli az egységek támadó értékét
- sanitation: az egységek harc után visszagyógyulnak a városok területén

- harbor: elérhetővé teszi a kikötőt
- farming: elérhetővé teszi a farmot
- mining: elérhetővé teszi a nyersanyag termelőt a hegy típusú mezőkön
- forestry: elérhetővé teszi a nyersanyag termelőt az erdő típusú mezőkön
- gem mining: elérhetővé teszi a nyersanyag termelőt a sivatag típusú mezőkön
- banking: elérhetővé teszi a bank épületet

- irrigation: növeli a farm élelem termelését
- stock market: növeli a bank termelését
- industrial revolution: növeli a nyersanyag termelők termelését
- mathematics: csökkenti az épületek építésének a költségét

## Bonyolultabb szabályok

Technológiák
- scouting: eggyel növeli az egység látótávolságát
- street: utat lehet építeni, amin az egységek gyorsabban tudnak közlekedni
- hunting: megjeleníti a térképen található állatokat, az állatok levadászása plusz élelmet jelent
- science: kap egy tudóst, aki valami egyedit tud (egyedi egység, egyedi épület, egyedi technológia)
- city wall: elérhetővé teszi a városfal építését, amely a város védelmét növeli
- diplomacy: egy egység fölényes túlerőben való megtámadásakor az egység átáll
- castle: elérhetővé teszi a várat
- trading: a városok közti nyersanyag cserét teszi lehetővé (minden nyersanyagot egyben tudunk kezelni)

Egységek
- barbár horda: barbár tábort védi, 1 egységet tud mozogni

Épületek
- városfal: a városban lévő egységek védelmét növeli
- vár: védelmi erődítmény, a benne lévő egység védelmét növeli
- rom: nyersagot ad, felderítés után eltűnik
- barbár tábor: egy barbár egység védi, nyersanyagot és pénzt ad
- oltár: képességet ad

---

## Log

* nem érdemes azonosító alapján
    * nincs rá garancia, hogy megint ugyanazokat osztja ki a modell
    * minden mezőn legfeljebb egy épület és egy egység, elég a koordináta
    * ha a view osztja ki, akkor meg felesleges
* minden `Action`-höz külön feldolgozó metódus //=> LogDataWrapper-ben
    * mivel más paraméterekre van szükségük, ezért saját data class
* érdemes cachelni a logot
    * minden `Action` után töröljük a fájl tartalmát és kiloggoljuk memóriából
        * egyszerűbb, mint egy json tömbbe elemeket beszúrni
        * vagy átnevezzük az régi fájlt, mentünk és töröljük a régit
* minden logfájl neve a kezdés időpontja
    * pl. `2023-19-31_11-19-12.json`
* `ActionParameters` mezőt érdemes lehet egységesíteni
    * értsd: `Action`-től függetlenül megmarad az összes lehetséges mező
    * problémás, ha túl sok típusa lesz `Action`-nek
* példa egy logfájl tartalmára:
```json
{
    "Settings" : "<filepath>",
    "Map" : "<filepath>",
    "Players" : [
        {
            "Name" : "Alice",
            "StartingTile" : [14, 12]
        },
        {
            "Name" : "Bob",
            "StartingTile" : [0, 0]
        }
    ],
    "Actions" : [
        {
            "Player" : "Alice",
            "Action" : "train",
            "ActionParameters" : {
                "Coord" : [5, 5],
                "Troop" : "builder"
            }
        },
        {
            "Player" : "Alice",
            "Action" : "move",
            "ActionParameters" : {
                "SourceCoord" : [5, 5],
                "TargetCoord" : [5, 6]
            }
        },
        {
            "Player" : "Alice",
            "Action" : "build",
            "ActionParameters" : {
                "Coord" : [5, 6],
                "Building" : "supplier"
            }
        },
        {
            "Player" : "Alice",
            "Action" : "finishturn",
            "ActionParameters" : {}
        },
        {
            "Player" : "Bob",
            "Action" : "build",
            "ActionParameters" : {
                "SourceCoord" : [5, 6],
                "TargetCoord" : [5, 5]
            }
        }
    ]
}
```

## Beállítások
* minden beállítható tulajdonság egy fájlban
```json
{
    "BaseProduction" : {
        "Money" : 1000,
        "Material" : 1000,
        "Food" : 1000
    },
    "BuildingProperties" : {
        "Bank" : {
            "Cost" : {
                "Food" : 10,
                "Material" : 10,
                "Money" : 10
            },
            "Health" : 50,
            "ProductionRate" : 10
        },
        "City" : {
            "Cost" : {
                "Food" : 20,
                "Material" : 20,
                "Money" : 20
            },
            "Health" : 50,
            "ProductionRate" : 100
        },
        "Farm" : {
            "Cost" : {
                "Food" : 10,
                "Material" : 10,
                "Money" : 10
            },
            "Health" : 50,
            "ProductionRate" : 10
        },
        "Harbor" : {
            "Cost" : {
                "Food" : 20,
                "Material" : 20,
                "Money" : 20
            },
            "Health" : 50,
            "ProductionRate" : 15
        },
        "Supplier" : {
            "Cost" : {
                "Food" : 40,
                "Material" : 40,
                "Money" : 40
            },
            "Health" : 50,
            "ProductionRate" : 10
        }
    },
    "TroopProperties" : {
        "Archer" : {
            "Cost" : {
                "Food" : 50,
                "Material" : 10,
                "Money" : 20
            },
            "Health" : 6,
            "Damage" : 3,
            "MovementRange" : 3,
            "AttackRange" : 2,
            "DodgeRate" : 0.3
        },
        "Boat" : {
            "Cost" : {
                "Food" : 120,
                "Material" : 120,
                "Money" : 50
            },
            "Health" : 12,
            "Damage" : 4,
            "MovementRange" : 3,
            "AttackRange" : 3,
            "DodgeRate" : 0.2
        },
        "Builder" : {
            "Cost" : {
                "Food" : 0,
                "Material" : 0,
                "Money" : 0
            },
            "Health" : 3,
            "Damage" : 0,
            "MovementRange" : 3,
            "AttackRange" : 0,
            "DodgeRate" : 0.3
        },
        "Catapult" : {
            "Cost" : {
                "Food" : 100,
                "Material" : 70,
                "Money" : 10
            },
            "Health" : 15,
            "Damage" : 8,
            "MovementRange" : 1,
            "AttackRange" : 5,
            "DodgeRate" : 0.0
        },
        "Scout" : {
            "Cost" : {
                "Food" : 20,
                "Material" : 20,
                "Money" : 20
            },
            "Health" : 4,
            "Damage" : 3,
            "MovementRange" : 3,
            "AttackRange" : 1,
            "DodgeRate" : 0.4
        },
        "Settler" : {
            "Cost" : {
                "Food" : 10,
                "Material" : 10,
                "Money" : 10
            },
            "Health" : 3,
            "Damage" : 0,
            "MovementRange" : 3,
            "AttackRange" : 0,
            "DodgeRate" : 0.2
        },
        "Warrior" : {
            "Cost" : {
                "Food" : 10,
                "Material" : 10,
                "Money" : 30
            },
            "Health" : 10,
            "Damage" : 5,
            "MovementRange" : 1,
            "AttackRange" : 1,
            "DodgeRate" : 0.1
        }
    },
    "TechTreeItemCosts" : {
        "Archery" : {
            "Food" : 10,
            "Material" : 10, 
            "Money" : 10
        },
        "Banking" : {
            "Food" : 10,
            "Material" : 10, 
            "Money" : 10
        },
        "Catapult" : {
            "Food" : 40,
            "Material" : 40, 
            "Money" : 40
        },
        "Farming" : {
            "Food" : 10,
            "Material" : 10, 
            "Money" : 10
        },
        "Forestry" : {
            "Food" : 10,
            "Material" : 10, 
            "Money" : 10
        },
        "GemMining" : {
            "Food" : 20,
            "Material" : 20, 
            "Money" : 20
        },
        "Harbor" : {
            "Food" : 20,
            "Material" : 20, 
            "Money" : 20
        },
        "IndustrialRevolution" : {
            "Food" : 30,
            "Material" : 30, 
            "Money" : 30
        },
        "Irrigation" : {
            "Food" : 20,
            "Material" : 20, 
            "Money" : 20
        },
        "Mathematics" : {
            "Food" : 20,
            "Material" : 20, 
            "Money" : 20
        },
        "Militarism" : {
            "Food" : 30,
            "Material" : 30, 
            "Money" : 30
        },
        "Mining" : {
            "Food" : 20,
            "Material" : 20, 
            "Money" : 20
        },
        "Navigation" : {
            "Food" : 40,
            "Material" : 40, 
            "Money" : 40
        },
        "Riding" : {
            "Food" : 20,
            "Material" : 20, 
            "Money" : 20
        },
        "Sailing" : {
            "Food" : 30,
            "Material" : 30, 
            "Money" : 30
        },
        "Sanitation" : {
            "Food" : 40,
            "Material" : 40, 
            "Money" : 40
        },
        "StockMarket" : {
            "Food" : 30,
            "Material" : 30, 
            "Money" : 30
        },
        "Strategy" : {
            "Food" : 40,
            "Material" : 40, 
            "Money" : 40
        }
    }
}
```
* a pályageneráláshoz szükséges értékek külön
* ez logghoz nem kell
```json
{
    "MinMountainCount" : 3,
    "MaxMountainCount" : 5,
    "MinForestCountPerchunk" : 3,
    "MaxForestCountPerchunk" : 5,
    "DesertChunkProbability" : 0.1,
    "WaterTileProbability" : 0.3
}
```