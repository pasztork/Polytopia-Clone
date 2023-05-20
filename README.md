# Polytopia-Clone
Ez a projekt "Önálló laboratóriumra" készült. A "The Battle of Polytopia" c. játék egy másolata.


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
- minden beállítható tulajdonság külön &rarr; [`PropertiesSettings.json`](/Polytopia%20Clone/GameSettings/PropertiesSettings.json)
- a pályageneráláshoz szükséges értékek külön &rarr; [`MapGenerationSettings.json`](/Polytopia%20Clone/GameSettings/MapGenerationSettings.json)


## Hálózat
- websocket alapú kommunikáció
- a klienst azután regisztrálja a szerver, hogy az az alábbi formátumú üzenetet elküldi:
    ```json
    {
        "Name": "ide jön a kliens neve (string)"
    }
    ```
- csatlakozás után a szerver visszaküldi a játék beállításait a lenti formában
    ```json
    {
        "Tiles": ...a pálya leírása...,
        "Settings": ...a játék beállításai...
    }
    ```
- a kliensek számát előre beállítjuk
    - amikor a kliensek száma eléri ezt a számot, a játék elindul
- értesíti a soron következő klienst az alábbi formátumú üzenettel:
    ```json
    {
        "Action": "StartTurn",
        "Name": ...ide jön a soron következő kliens neve...
    }
    ```
- a ,,[Parancsok](#parancsok)'' c. fejezetben leírt parancsokat tudja feldolgozni a szerver
    - a kliens egyenként küldhet parancsokat 
- minden validált parancsot az összes kliensnek (kivéve, amelyik kiadta a parancsot) továbbítja a szerver
- amikor a kliens végzett a körével, `EndTurn` parancsot küld, ekkor a szerver értesíti a következő klienst
- amikor az egyik kliens győz, a szerver felbontja a kapcsolatot az összes klienssel


## Parancsok
- a websocketeknek json formátumú parancsokat lehet küldeni
- ezekkel a parancsokkal lehet vezérelni a játékot
- az elérhető parancsok a következők
    - `AttackBuilding`
    - `AttackTroop`
    - `Build`
    - `EndTurn`
    - `GetGameState` &rarr; lekéri a játék aktuális állapotát
    - `Learn`
    - `Move`
    - `Train`