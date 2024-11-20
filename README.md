Ovaj projekat je API za upravljanje igračima, timovima i statistikom unutar simulirane baze podataka. Projekat je razvijen korišćenjem C# i ASP.NET Core, sa integrisanom podrškom za validaciju, lazy loading, i testiranje u in-memory bazi.

Potrebno okruženje za build:
Za uspešan build aplikacije potrebno je sledeće:

.NET SDK verzije 6.0
IDE


Za build aplikacije koristite sledeću komandu u root direktorijumu projekta:

dotnet build

Pokretanje aplikacije:

Otvorite projekat u Visual Studio ili Visual Studio Code.

Postavite API projekat kao Startup Project.

Pritisnite F5 ili pokrenite aplikaciju koristeći opciju Start Debugging.


Tehnologije korišćene u projektu:

1)ASP.NET Core:

Osnovni framework za izgradnju web API-ja.


2) Entity Framework Core:

ORM za rad sa bazama podataka.

3) Microsoft.EntityFrameworkCore.InMemory:

Koristi se za simulaciju baze podataka tokom razvoja i testiranja.

4) Microsoft.EntityFrameworkCore.Proxies:

Omogućava lazy loading navigacionih svojstava.

5) FluentValidation:

Biblioteka za deklarativnu validaciju podataka.
