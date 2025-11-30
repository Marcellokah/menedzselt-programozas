# Zenehallgatás Alkalmazás

## Áttekintés
Ez a projekt egy C# nyelven írt, MVC tervezési mintát követő alkalmazás, amely lehetővé teszi zenék nyilvántartását. A megoldás két felhasználói felületet biztosít:
1. **WinForms Desktop alkalmazás**: Teljes funkcionalitás (Hozzáadás, Böngészés, Prioritás módosítása).
2. **ASP.NET Core MVC Webes felület**: Böngészés és Hozzáadás.

Az adatok perzisztenciáját SQLite adatbázis biztosítja Entity Framework Core segítségével.

## Funkcionalitások
- **Új zene hozzáadása**: Cím (egyedi), Előadó, Év, Hossz, Prioritás megadása.
- **Böngészés**: Lista megtekintése prioritás szerint csökkenő sorrendben (maga a prioritás szám rejtve marad).
- **Módosítás (Csak WinForms)**: A listában duplán kattintva egy elemre a prioritás módosítható.

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