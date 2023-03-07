# Polytopia-Clone

Ez a projekt "Önálló laboratóriumra" készült. A "The Battle of Polytopia" c. játék egy másolata.

## TODO

- [x] egy canvas az összes mezőhöz
- [ ] pontrendszer kialakítása
- [ ] seregek mozgatása
- [ ] kezdő építmény elhelyezése
=======
### Alap szabályok
---
Területek
 - viz
 - mező: sík mezőnek számít
 - hegy: magaslati mezőnek számít
 - sivatag: sík mezőnek számit
 Egy mezőn csak egy egység tartózkodhat egy időben.

Fizetőeszközök
- pénz: körönként termelődik fix mennyiség
- nyersanyag: ha a város területén van hegy, akkor körönként termelődik fix mennyiség
- élelem: ha a város területén van mező és/vagy víz akkor körönként fix mennyiség termelődik

Egységek
- felderitő: 3 egységet tud mozogni alapból, harcolni, megszálni törzseket nem tud
- zsoldos: 1 egységet tud mozogni alapból, körönként egy élelembe kerülnek
- íjász: 2 egységet tud mozogni alapból, körönként egy élelembe kerülnek
- hajó: 2 egységet tud alabol mozogni, alapból kikötőben jön létre, egységek le-fel szállni róla csak sík mezőn tudnak
- telepes: 2 egyég a mozgása, új várost tud létrehozni
- épitész: épületeket tud építeni
- barbár horda: barbár tábort védi, 1 egységet tud mozogni

Épületek
- város: van alaptermelése mind 3 alap nyersanyagból
- főváros: a kezdő város
- rom: nyersagot ad, felderítés után eltünik
- barbár tábor: egy barbár egység védi, nyersanyagot és pénzt ad
- oltár: képességet ad
- nyersanyag termelő (bánya, fatelep): nyersanyagot termel körönként
- bank: pénzt termel körönként
- farm: élelmet termel körönként

Technológiák, képességek
- archery: elérhetővé teszi az íjászt
- sailing: elérhetővé teszi a hajót
- farming: elérhetővé teszi a farmot
- hunting: állatokat vesz észre, melyet le tud vadászni, az állatok levadászáda plusz élélmet jelent
- mining: elérhetővé teszi a nyersanyag termelőt, a hegyen
- forestry: elérhetővé teszi a nyersanyag termelőt, a mezőn
- gem mining: elérhetővé teszi a nyersanyag termelőt, a sivatagban
- banking: elérhetővé teszi a bankot
- riding: növeli a szárazfoldi egységek mozgását egygel
- navigation: növeli a hajók mozgását eggyel

- mathematics: csökkenti az épületek építésének a költségét
- scouting: növeli az egység látó távolságát eggyel
- strategy: növeli az egységek védekező értékét
- street: utat lehet építeni
- öntözés: növeli a város élelem termelését



### Bonyolultabb szabályok
---
Technológiák
- minden egységnek a támadó értékét is növelő fejlesztés

- science: kap egy tudost, aki valami egyedit tud (egyedi egység, egyedi épület, egyedi technológia)
- citywall: város falat tud építen, mely a város védelmét növeli
- catapulting: elérhetővé teszi a katapultot 
- sanitation: az egységek harc után visszagyogyulnak
- diplomacy: ha fölényes túlerőben támad meg egy egységet akkor azok átálnak
- castle: elérhetővé teszi a várat
- trading: a városok közti nyersanyag cserét teszi lehetővé (minden nyersanyagot egyben tudunk kezelni)

Egységek
- katapult: védelmi épületek (városfal, vár) rombolására jó

Épületek
- városfal: a városban lévő egységek védelmét növeli
- vár: védelmi erőditmény, a benne lévő egység védelmét növeli
