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
* minden beállítható tulajdonság külön &rarr; [`PropertiesSettings.json`](/Polytopia%20Clone/GameSettings/PropertiesSettings.json)
* a pályageneráláshoz szükséges értékek külön &rarr; [`MapGenerationSettings.json`](/Polytopia%20Clone/GameSettings/MapGenerationSettings.json)