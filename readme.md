Webbapp som använder googles Geocoding API för att hämta koordinater baserat på adressen som en användare skriver in.

Innan du kör programmet, gå in i appsettings.json och ändra värdet på Data Source i "ConnectionString" till namnet för din egen maskin/server.
Kör sedan programmet med IIS Expresss. Databasen kommer sen att skapas samt populeras med lite testdata när du kör programmet.
Gå in på "Stores" och klicka på "Create new store" för att skapa en ny butik. Skriv in en adress och klicka på "Create".
Koordinater för adressen kommer att hämtas från Googles Geocoding API och sparas i databasen.

Frontend-biten är en work in progress. För närvarande består den enbart av av scaffoldade Razor Pages.
Fokus har legat på backend-delen och dess interaktion med Googles Geocoding API.
