# Polytopia-Clone

Ez a projekt "Önálló laboratóriumra" készült. A "The Battle of Polytopia" c. játék egy másolata.

## TODO

- [x] egy canvas az összes mezőhöz
- [ ] pontrendszer kialakítása
- [ ] seregek mozgatása
- [ ] kezdő építmény elhelyezése
=======
### Alapszabályok
---
Területek
 - víz
 - mező: sík mezőnek számít
 - hegy: magaslati mezőnek számít
 - sivatag: sík mezőnek számít
 Egy mezőn csak egy egység tartózkodhat egy időben.

Fizetőeszközök
- pénz: körönként termelődik fix mennyiség
- nyersanyag: ha a város területén van hegy, akkor körönként termelődik fix mennyiség
- élelem: ha a város területén van mező és/vagy víz akkor körönként fix mennyiség termelődik

Egységek
- felderítő: 3 egységet tud mozogni alapból, harcolni, megszálni törzseket nem tud
- zsoldos: 1 egységet tud mozogni alapból, körönként egy élelembe kerül
- íjász: 2 egységet tud mozogni alapból, körönként egy élelembe kerül
- hajó: 2 egységet tud alapból mozogni, alapból kikötőben jön létre, egységek le-fel szállni róla csak sík mezőn tudnak
- telepes: 2 egyég a mozgása, új várost tud létrehozni
- építész: épületeket tud építeni
- barbár horda: barbár tábort védi, 1 egységet tud mozogni

Épületek
- város: van alaptermelése mindhárom alap nyersanyagból
- főváros: a kezdő város
- rom: nyersagot ad, felderítés után eltűnik
- barbár tábor: egy barbár egység védi, nyersanyagot és pénzt ad
- oltár: képességet ad
- nyersanyag termelő (bánya, fatelep): nyersanyagot termel körönként
- bank: pénzt termel körönként
- farm: élelmet termel körönként

Technológiák, képességek
- archery: elérhetővé teszi az íjászt
- sailing: elérhetővé teszi a hajót
- farming: elérhetővé teszi a farmot
- hunting: megjeleníti a térképen található állatokat, az állatok levadászása plusz élelmet jelent
- mining: elérhetővé teszi a nyersanyag termelőt a hegy típusú mezőkön
- forestry: elérhetővé teszi a nyersanyag termelőt a mező típusú mezőkön
- gem mining: elérhetővé teszi a nyersanyag termelőt a sivatag típusú mezőkön
- banking: elérhetővé teszi a bank épületet
- riding: eggyel megnöveli a szárazföldi egységek mozgását
- navigation: eggyel megnöveli a hajók mozgását
- mathematics: csökkenti az épületek építésének a költségét
- scouting: eggyel növeli az egység látótávolságát
- strategy: növeli az egységek védekező értékét
- street: utat lehet építeni, amin az egységek gyorsabban tudnak közlekedni
- irrigation: növeli a város élelem termelését



### Bonyolultabb szabályok
---
Technológiák
- militarism: növeli az egységek támadó értékét
- science: kap egy tudóst, aki valami egyedit tud (egyedi egység, egyedi épület, egyedi technológia)
- city wall: elérhetővé teszi a városfal építését, amely a város védelmét növeli
- catapulting: elérhetővé teszi a katapult egységet, ami extra sebzést ad a városfalak ellen
- sanitation: az egységek harc után visszagyógyulnak a városok területén kívül is
- diplomacy: egy egység fölényes túlerőben való megtámadásakor az egység átáll
- castle: elérhetővé teszi a várat
- trading: a városok közti nyersanyag cserét teszi lehetővé (minden nyersanyagot egyben tudunk kezelni)

Egységek
- katapult: védelmi épületek (városfal, vár) rombolására jó

Épületek
- városfal: a városban lévő egységek védelmét növeli
- vár: védelmi erődítmény, a benne lévő egység védelmét növeli
