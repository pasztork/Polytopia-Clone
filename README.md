# Terra Imperium

Ez a projekt **Önálló laboratórium**ra készült. A **Civilization** és a **The Battle of Polytopia** című játékok alapján.

## Tartalomjegyzék

1. [Alapszabályok](#alapszabályok)
2. [Beállítások](#beállítások)
3. [Kliens készítése](#kliens-készítése)
4. [Elérhető parancsok](#elérhető-parancsok)

## Alapszabályok

Mezők
- víz:
    - hajó, építész, valamint telepes egységek közlekedhetnek rajta
    - kikötőt lehet rá építeni
- síkság:
    - minden szárazföldi egység közlekedhet rajta
    - minden szárazföldi épületet lehet rá építeni
- erdő:
    - minden szárazföldi egység közlekedhet rajta
    - minden szárazföldi épületet lehet rá építeni
- sivatag:
    - minden szárazföldi egység közlekedhet rajta
    - minden szárazföldi épületet lehet rá építeni, kivéve farmot
- hegy:
    - építész, valamint telepes egységek közlekedhetnek rajta
    - csak nyersanyag termelő építhető rá (technológia szükséges)

Fizetőeszközök:
- pénz:  
    - a városok, valamint a területükön lévő bankok és kikötők termelik
- nyersanyag:  
    - a városok, valamint a területükön lévő nyersanyag termelők termelik
- élelem: 
    - a városok, valamint a területükön lévő farmok és kikötők termelik

Egységek
- felderítő: 
    - offenzív szárazföldi egység
    - közelharci támadása van 
    - nagy mozgási távolság, de kevés élet és sebzés
    - városban lehet létrehozni
- harcos: 
    - offenzív szárazföldi egység
    - közelharci támadása van
    - kis mozgási távolság, de sok élet és sebzés
    - városban lehet létrehozni
- íjász: 
    - offenzív szárazföldi egység
    - távolsági támadása van
    - nagy mozgási távolság, valamint sebzés, de kevés élet
    - városban lehet létrehozni
    - technológia kifejlesztése szükséges
- hajó:
    - offenzív vízi egység
    - távolsági támadása van
    - kikötőben lehet létrehozni
    - technológia kifejlesztése szükséges
- telepes:
    - passzív egység, városokat lehet vele alapítani
    - egy telepes 1 várost tud létrehozni, ezután meghal
    - városban lehet létrehozni
    - kevés élettel rendelkezik
    - szárazföldön és vízen is tud közlekedni
- építész:
    - passzív egység, épületeket tud építeni a városok köré
    - egy építész 1 épületet tud építeni, ezután meghal
    - városban lehet létrehozni
    - kevés élettel rendelkezik
    - szárazföldön és vízen is tud közlekedni
- katapult: 
    - offenzív szárazföldi egység
    - távolsági támadása van
    - kis mozgási távolság, viszont és nagy sebzés sok élet 
    - a megtámadott mező szomszédjait is sebzi, feleannyi sebzéssel
    - városban lehet létrehozni
    - technológia kifejlesztése szükséges

Épületek
- város:
    - szárazföldi épület
    - mindhárom fizetőeszközt termeli körönként
    - telepes hozhatja létre (kivéve a fővárost)
    - szárazföldi egységeket tud létrehozni
    - a szomszédos mezőkre:
        - lehet épületeket építeni
        - nem lehet újabb város létrehozni
- főváros:
    - a kezdő város, a játék helyezi el a térképen
    - mindenben ugyanolyan, mint a többi város
- nyersanyag termelő:
    - szárazföldi épület, de hegyre is építhető (technológia szükséges)
    - nyersanyagot termel körönként
    - bizonyos mezőkre csak technológia kifejlesztése után lehet elhelyezni
    - építész építi meg
- bank:
    - szárazföldi épület
    - pénzt termel körönként
    - építész építi meg
- farm:
    - szárazföldi épület, viszont sivatagra nem lehet lerakni
    - élelmet termel körönként
    - építész építi meg
- kikötő:
    - vízi épület
    - pénzt és élelmet termel körönként
    - hajókat tud létrehozni
    - építész építi meg

Technológiák, képességek
- íjászat (archery): 
    - elérhetővé teszi az íjász egységet
- katapultálás (catapulting): 
    - elérhetővé teszi a katapult egységet, ami extra sebzést ad a városfalak ellen
- vitorlázás (sailing): 
    - elérhetővé teszi a hajó egységet
- lovaglás (riding): 
    - eggyel megnöveli a harcos egységek mozgási távolságát
- navigáció (navigation): 
    - eggyel megnöveli a hajók mozgását
- stratégia (strategy): 
    - növeli a támadás elhárításának valószínűségét
- fegyverkezés (militarism): 
    - növeli az egységek támadó értékét
- higiénia (sanitation): 
    - az egységek harc után visszagyógyulnak a városok területén
- kikötő (harbor): 
    - elérhetővé teszi a kikötőt
- gazdálkodás (farming): 
    - elérhetővé teszi a farmot
- bányászás (mining): 
    - elérhetővé teszi a nyersanyag termelőt a hegy típusú mezőkön
- erdészet (forestry): 
    - elérhetővé teszi a nyersanyag termelőt az erdő típusú mezőkön
- drágakő bányászat (gem mining): 
    - elérhetővé teszi a nyersanyag termelőt a sivatag típusú mezőkön
- bankügylet (banking): 
    - elérhetővé teszi a bank épületet
- öntözés (irrigation): 
    - növeli a farm élelem termelését
- tőzsde (stock market): 
    - növeli a bank termelését
- ipari forradalom (industrial revolution): 
    - növeli a nyersanyag termelők termelését
- matematika (mathematics): 
    - csökkenti az épületek építésének a költségét

Egy körben egy játékos:
- minden egységével legfeljebb egyszer léphet, valamint támadhat
- minden városban, kikötőben egy egységet képezhet ki
- bármennyi technológiát tanulhat

További szabályok:
- egy mezőn egy időben legfeljebb csak 1 egység tartózkodhat, valamint legfeljebb csak 1 épületet lehet rá építeni
- egységeket csak épületek mezőjére lehet létrehozni
- épületeket csak a városok körzetébe lehet építeni
- egy város körzetében nem lehet másik várost létrehozni
- ha egy város megsemmisül, akkor az összes, a körzetében lévő épület is megsemmisül
- minden egységnek, épületnek, technológiának van egy költsége, amennyibe kerül a létrehozása vagy kifejlesztése

Győzelem:
- ha egy játékosnak az összes városa megsemmisül, akkor kiesik a játékból
- az a játékos nyer, aki nem esik ki

## Beállítások

- minden beállítható tulajdonság külön &rarr; [`PropertiesSettings.json`](/Resources/GameSettings/PropertiesSettings.json)
- a pályageneráláshoz szükséges értékek külön &rarr; [`MapGenerationSettings.json`](/Resources/GameSettings/MapGenerationSettings.json)

## Kliens készítése

A kommunikáció websocketen keresztül történik, minden, a szerver által küldött üzenet első kulcsa a *Type* mező, amely megadja az üzenet típusát.

Amennyiben a szerver már fut egy előre ismert porton, a kliens csatlakozhat rá.

A legelső üzenetben regisztárlnia kell egy játékost (egy websocketről csak egy játékost fogad).
Ezt az alábbi formátumú json üzenettel teheti meg.
```json
{
    "Name": ...ide jön a kliens neve (string)...
}
```

Regisztráció után a szerver elküldi a pályát, illetve a felhasznált játékbeállításokat.
Előbbi egy két dimenziós tömbben érkezik, koordinátahelyesen.
A beállítások értékei pedig egy *Setup* típusú üzenetben, [ehhez](/Resources/GameSettings/PropertiesSettings.json) a fájlhoz teljesen hasonlóan érkeznek.
A megkapott json üzenet formája alább látható.
```json
{
    "Type": "Setup",
    "Tiles": ...a pálya leírása (array)...,
    "Settings": ...a játék beállításai (object)...
}
```
A későbbiekben az egyes mezőkre a tömbben megkapott helye alapján tudunk hivatkozni (0 alapú index).
Nem csak a mezőkre, de a rajtuk található épületekre, egységekre is ezekkel a koordinátákkal kell hivatkozni.

A kliensek száma előre ismert.
Amint regisztrált elég felhasználó, elindul a játék.

A soron következő játékos mindig a következő üzenetet kapja meg.
```json
{
    "Type": "StartTurn",
    "Name": ...ide jön a soron következő kliens neve (string)...
}
```

A kliens az [Elérhető parancsok](#elérhető-parancsok) című fejezetben leírt parancsokkal irányíthatja a játékot.

Amikor a kliens végez a körével a következő üzenetet kell elküldje a szervernek, aminek hatására a vezérlést megkapja a következő kliens.
```json
{
    "Name": ...ide jön a kliens neve (string)...,
    "Action": "EndTurn"
}
```

## Elérhető parancsok

A kliensek a következő típusú parancsokat adhatják ki (minden mást elutasít a szerver):
- `AttackBuilding`
- `AttackTroop`
- `Build`
- `EndTurn`
- `GetActions`
- `GetGameState`
- `Learn`
- `Move`
- `Train`

Minden üzenetet json formátumban kell elküldeni a szervernek.
Az összes üzenetnek illeszkednie kell az alábbi formára:
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": ...ide jön a parancs neve (string)...,
    "Parameters": {
        "Start": ...ide jön a parancs kezdő koordinátája (2 elemű tömb)...,
        "End": ...ide jön a parancs végső koordinátája (2 elemű tömb)...,
        "Troop": ...ide jön az egység típusa (string)...,
        "Building": ...ide jön az épület típusa (string)...,
        "Tech": ...ide jön a technológia típusa (string)...
    }
}
```
Természetesen nem kell minden parancshoz minden paramétert kitölteni.
Ha egy paraméterre nincs szükség, akkor az a szerver nem figyeli, ki lehet tölteni, el lehet hagyni, nem befolyásolja a működést.

A `Name` paraméterbe mindig meg kell adni a játékos nevét.
Ezt nem figyeli a szerver, de a naplófájlok olvashatósága végett, kérnénk minden klienst, hogy írja bele a nevét.

Az `Action` paraméterben a fenti listában szereplő parancsok nevének egyike kell szerepelnie.
Az egyes parancsok használatáról lejebb olvashatunk.

Az `AttackBuilding` parancs esetén a paraméterekből a `Start` és `End` mindenképpen kitöltendő.
Előbbibe annak az egységnek a koordinátáit kell megadni, amelyikkel támadni szeretnénk.
Utóbbi annak az épületnek a koordinátáit kell megadni, amelyiket meg szeretnénk támadni.
Erre lentebb láthatunk egy példát.
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": "AttackBuilding",
    "Parameters": {
        "Start": [0, 0],
        "End": [1, 1]
    }
}
```

Az `AttackTroop` parancs esetén a paraméterekből a `Start` és `End` a mindenképpen kitöltendő mezők.
Előbbibe annak az egységnek a koordinátáit kell megadni, amelyikkel támadni szeretnénk.
Utóbbi annak az egységnek a koordinátáit kell megadni, amelyiket meg szeretnénk támadni.
Erre lentebb láthatunk egy példát.
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": "AttackTroop",
    "Parameters": {
        "Start": [0, 0],
        "End": [1, 1]
    }
}
```

A `Build` parancs esetén a paraméterekből a `Start` és `Building` a mindenképpen kitöltendő mezők.
Előbbibe kerül annak az egységnek a koordinátái, amellyel az épületet szeretnénk létrehozni, utóbbiba pedig a létrehozandó épület típusa.
Erre lentebb láthatunk egy példát.
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": "Build",
    "Parameters": {
        "Start": [0, 0],
        "Building": ...ide jön az építeni kívánt épület típusa (string)...
    }
}
```

Az `EndTurn` parancsot akkor kell kiadnia a kliensnek, ha végzett a körével.
Ekkor a szerver elveszi tőle a vezérlést és átadja a következő felhasználónak.
Erre az üzenetre az alábbi formátú üzenetet kell elküldeni.
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": "EndTurn"
}
```

A `GetActions` parancsot arra használhatja a kliens, hogy lekérdezze az általa aktuálisan végrehajtható lépéseket. 
Ez az alábbi üzenettel kérdezhető le.
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": "GetActions"
}
```
A szerver a válaszában felsorolja, hogy a kliens milyen egységével hova tud lépni, támadó egység esetén milyen más egységeket tud megtámadni, passzív egységeknél pedig a megépíthető épületeket sorolja fel. Emellett tartalmazza az épületeket, hogy azokkal milyen egységeket tud létrehozni, valamint a megtanulható technológiákat is. Ebben a válaszban NEM minden, a játékoshoz tartozó egység és épület kerül felsorolásra, csak azok, amelyekkel érvényes cselekvést tud végrehajtani, figyelembe véve a játékos fizetőeszközeinek mennyiségét is.
Egy ilyen válaszra lentebb láthatunk egy példát.
```json
{
    "Type": "ActionState",
    "Troops": [
        {
            "Type": "Warrior",
            "Health": 10,
            "Damage": 5,
            "Position": [0, 0],
            "TilesToMove": [
                [0, 1],
                [1, 0]
            ],
            "TroopsToAttack": [
                {
                    "Name": "Bob",
                    "Position": [1, 1],
                    "Type": "Warrior",
                    "Health": 5,
                    "Damage": 10
                }
            ],
            "BuildingsToAttack": [
                {
                    "Name": "Bob",
                    "Position": [1, 1],
                    "Type": "City",
                    "Health": 30,
                    "Damage": 0
                }
            ],
            "BuildingsToBuild": []
        },
        {
            "Type": "Builder",
            "Health": 5,
            "Damage": 0,
            "Position": [5, 5],
            "TilesToMove": [
                [4, 5],
                [5, 4],
                [5, 6],
                [6, 5]
            ],
            "TroopsToAttack": [],
            "BuildingsToAttack": [],
            "BuildingsToBuild": [
                {
                    "Type": "Farm",
                    "Cost": {
                        "FoodCost": 10,
                        "MaterialCost": 10,
                        "MoneyCost": 10                    
                    }
                },
                {
                    "Type": "Supplier",
                    "Cost": {
                        "FoodCost": 20,
                        "MaterialCost": 20,
                        "MoneyCost": 20                    
                    }
                }
            ]
        }
    ],
    "Buildings": [
        {
            "Type": "City",
            "Health": 30,
            "Position": [5, 5],
            "TroopsToTrain": [
                {
                    "Type": "Warrior",
                    "Cost": {
                        "FoodCost": 20,
                        "MaterialCost": 20,
                        "MoneyCost": 20                    
                    }
                },
                {
                    "Type": "Archer",
                    "Cost": {
                        "FoodCost": 50,
                        "MaterialCost": 50,
                        "MoneyCost": 50                    
                    }
                }
            ]
        }
    ],
    "TechsToUnlock": [
        {
            "Name": "Forestry",
            "Cost": {
                "FoodCost": 10,
                "MaterialCost": 10,
                "MoneyCost": 10                    
            }
        },
        {
            "Name": "Riding",
            "Cost": {
                "FoodCost": 50,
                "MaterialCost": 50,
                "MoneyCost": 50                    
            }
        }
    ]
}
```

A `GetGameState` parancsot arra használhatja a kliens, hogy a játéktér aktuális állapotát kérje le.
Ezt az alábbi üzenettel kérheti le.
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": "GetGameState"
}
```
Erre a szerver válaszolni fog.
Felsorolja, hogy az egyes játékosoknak hol és milyen egysége, illetve épülete van, továbbá a már megtanult technológiákat is megkapja.
Egy ilyen válaszra lentebb láthatunk egy példát.
```json
{
    "Type": "GameState",
    "PlayerState": [
        {
            "Name": "Bob",
            "Banks": [],
            "Cities": [
                [10, 13]
            ],
            "Farms": [],
            "Harbors": [],
            "Suppliers": [],
            "Archers": [],
            "Boats": [],
            "Builders": [],
            "Catapults": [],
            "Scouts": [],
            "Settlers": [],
            "Warriors": [],
            "Techs": []
        },
        {
            "Name": "Alice",
            "Banks": [],
            "Cities": [
                [12, 4]
            ],
            "Farms": [],
            "Harbors": [],
            "Suppliers": [],
            "Archers": [],
            "Boats": [],
            "Builders": [],
            "Catapults": [],
            "Scouts": [],
            "Settlers": [],
            "Warriors": [],
            "Techs": []
        }
    ]
}
```
Az látható, hogy nem láthajuk, hogy mennyi élete maradt az egyes entitásoknak.
Ezt minden kliens saját magának kell számontartsa.
Amennyiben egy entitás elesik, akkor azt nem tünteti fel a válaszban a szerver.
A jövőben ezt szeretnénk a szerver felelősségei között tudni, addig a beállítások alapján mindenki maga döntheti el, hogy miképpen szeretné ezt kezelni.

A `Learn` parancs esetén a paraméterekből csak a `Tech` töltendő ki mindenképpen.
Ebbe kerül a megtanulni kívánt technológia típusa.
Erre lentebb láthatunk egy példát.
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": "Learn",
    "Parameters": {
        "Tech": ...ide jön a megtanulni kívánt technológia típusa (string)...
    }
}
```

Az `Move` parancs esetén a paraméterekből a `Start` és `End` a mindenképpen kitöltendő mezők.
Előbbibe annak az egységnek a koordinátáit kell megadni, amelyikkel lépni szeretnénk.
Utóbbi annak az üres mezőnek a koordinátái, ahová lépni szeretnénk.
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": "Move",
    "Parameters": {
        "Start": [0, 0],
        "End": [1, 1]
    }
}
```

A `Train` parancs esetén a paraméterekből csak a `Start` és `Troop` töltendő ki mindenképpen.
Előbbibe kerül annak az épületnek koordinátái, ahol az egységet kívánjuk létrehozni.
Utóbbiba pedig a létrehozandó egység típusa.
Erre lentebb láthatunk egy példát.
```json
{
    "Name": ...ide jön a játékos neve (string)...,
    "Action": "Train",
    "Parameters": {
        "Start": [0, 0],
        "Troop": ...ide jön a kiképezni kívánt egység típusa (string)...
    }
}
```
