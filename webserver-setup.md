# Webszerver használata

Ez a dokumentum arra szolgál, hogy bemutassa, hogyan kell elindítani, valamint használni az alkalmazást a webszerver komponensen keresztül.

## Tartalomjegyzék
1. [Szükséges eszközök, technológiák és verziók](#szükséges-eszközök-technológiák-és-verziók)
2. [Környezeti változó beállítása](#környezeti-változó-beállítása)
3. [Admin felvétele az adatbázisba](#admin-felvétele-az-adatbázisba)
4. [Mesterséges intelligencia feltöltése](#mesterséges-intelligencia-feltöltése)
5. [Verseny indítása](#verseny-indítása)
6. [Adminisztrációs menüpont](#adminisztrációs-menüpont)

## Szükséges eszközök, technológiák és verziók

* Visual Studio 2022
    * .NET 6.0
* Docker
    * Windows-on: Docker Desktop + WSL

## Környezeti változó beállítása

A projekt letöltését követően szükséges felvenni az operációs rendszeren egy, a fiókhoz tartozó környezeti változót, amely megadja az applikáció számára a játékot futtató szerver elérését. 

A környezeti változó formátuma:
* A változó neve: TERRA_IMPERIUM_SERVER_ADDRESS
* A változó értéke: <szerver_ip_címe>:53658

Ezt követően már elindítható a projekt.

## Admin felvétele az adatbázisba

A projekt indítását követően szükség lesz egy adminisztrátori jogokkal rendelkező felhasználó felvételére az adatbáziba.

Először a weboldalon érdemes regisztrálni egy új felhasználót a kívánt adatokkal. Sikeres regisztráció esetén az alkalmazás kiírja, hogy el kell fogadni a regisztrációt. 

Mivel ez lesz az első felhasználó a rendszerben, ezért a regisztráció elfogadását, valamint az adminisztrátori jogosultságot manuálisan kell beállítani. Ehhez a **Visual Studio**-n belül az **SQL Server Object Explorer**ben a **WebServerDB** adatbázisban jobb klikk az **AspNetUsers** táblán, majd *View Data*. A táblában láthatók az imént hozzáadott felhasználó adatai, itt kell az *EmailConfirmed* attribútum értékét *True*-ra állítani. Emellett az **AspNetUserRoles** táblában a *RoleId*-t kell kicserélni az **AspNetRoles** táblában, az *Admin*-hoz tartozó *Id*-ra.

Ezt követően már be lehet jelentkezni a létrehozott felhasználóval, és minden funkció elérhető lesz. 

## Mesterséges intelligencia feltöltése

Az **Upload AI** fülön lehetőség van feltölteni a megírt AI-ok fájljait *zip* formátumban, a fájlon belüli mappák szerkezetéről az alkalmazás feltételezi, hogy a repozitoriban található [`minta zip fájl`](/example-client/sample_ai.zip/) alapján van felépítve, röviden:
* minta_ai.zip
    * src
        * <ai fájlok>
    * Dockerfile
    * requirements.txt

## Verseny indítása

Versenyt indítani a **Tournament** fülön lehet. Itt ki kell választani a versenyben használni kívánt AI-ok fájljait, egy pályát, amin játszani fognak, valamint a verseny típusát:
* Leauge mode: minden kiválasztott AI minden másik AI-val a megadott számú mérkőzést játsza.
* Knockout mode: az AI-ok párokba rendezve játszanak, a nyertes továbbjut a következő körbe.

A verseny állását a weboldalon frissülő üzenetek, valamint a ponttáblázat jelzi, a lejátszott versenyek eredményei pedig megtekinthetők a **Results** oldalon.

## Adminisztrációs menüpont

Itt lehet a függőben levő regisztrációkat elfogadni, adminisztrátori jogot adni, vagy elvenni, valamint felhasználókat törölni.