# Zenehallgatás Alkalmazás

## Áttekintés
Ez a projekt egy C# nyelven írt, MVC tervezési mintát követő alkalmazás, amely lehetővé teszi zenék nyilvántartását. A megoldás két felhasználói felületet biztosít:
1. **WinForms Desktop alkalmazás**: Teljes funkcionalitás (Hozzáadás, Böngészés, Prioritás módosítása).
2. **ASP.NET Core MVC Webes felület**: Böngészés és Hozzáadás.

Az adatok perzisztenciáját SQLite adatbázis biztosítja Entity Framework Core segítségével.

## Funkcionalitások

### 1. Új zene hozzáadása
A felhasználó új zenét rögzíthet az adatbázisba. A következő adatokat kell megadni (Validációs szabályok):
- **Cím**: Szöveges adat. Megadása kötelező.
- **Előadó**: Szöveges adat. Megadása kötelező.
- **Kiadás éve**: Egész szám. Érvényes tartomány: 1 - 3000.
- **Hossz**: A zene hossza másodpercben megadva. Csak pozitív egész szám fogadható el.
- **Prioritás**: Egész szám 1 és 10 között.

### 2. Böngészés
A zenék listázása automatikusan történik.
- **Rendezés**: A lista a prioritás szerint csökkenő sorrendben jelenik meg.
- **Megjelenítés**: A prioritás konkrét számértéke a felhasználó elől rejtve marad a listanézetben.

### 3. Módosítás (Csak WinForms)
A már felvett zenék szerkesztése a listában található elemre való **dupla kattintással** kezdeményezhető.
- A felugró ablakban a kiválasztott zene adatai jelennek meg.
- A szerkesztés során csak a prioritás módosítható a validációs szabályok betartásával.

## Fejlesztői Dokumentáció (Dev)

### Követelmények
- .NET 8.0 SDK
- Visual Studio Code (vagy Visual Studio 2022)

### Projekt Felépítése
- **ZeneApp.Common**: Osztálykönyvtár. Tartalmazza a `Zene` modellt, a `ZeneContext` EF Core kontextust és a validációs logikát.
- **ZeneApp.WinForms**: A desktop felület logikája és formjai. Programozott UI (Code-behind) a VS Code kompatibilitás érdekében.
- **ZeneApp.Web**: A webes felület Controller-ei és View-i.

### Adatbázis
A `zenelista.sql` fájl tartalmazza a sémát. A program első futáskor automatikusan létrehozza a `zeneapp.db` SQLite fájlt (`EnsureCreated`).

## Felhasználói Dokumentáció (User)

### Telepítés és Futtatás

1. **Klónozás/Letöltés**: Másold a gépedre a forráskódot.
2. **Terminál**: Nyisd meg a mappát terminálban.

#### WinForms indítása:
```bash
dotnet run --project ZeneApp.WinForms
```

#### Webes felület indítása:
```bash
dotnet run --project ZeneApp.Web
```
