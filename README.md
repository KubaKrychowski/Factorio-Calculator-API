# Factorio-Calculator-API
The API for Factorio Calculator app project. Application is written with .NET Framework, PostgreSQL and EntityFramework Core. Tests are written with XUnit mixed with standard Microsoft library.
The Clean Code Architecture allows easy expansion and modernization of the app and the Test Driven Development lets save the easy way of optimizing and safe development without regresion.

## Entities

# Item
First and the primary entity ``` Item ```, its parted per categories like in game on:
- Vehicles
- Structures
- Parts
- Weapons
- Resources
Creating Item will be provided by ``` factory ``` design pattern, starting with common properties like name, stars, externalId etc. to more specific like: health; Width; height; etc. (details in analytics screen)
![item analytics](https://github.com/KubaKrychowski/Factorio-Calculator-API/assets/91949223/baf34d46-6b33-4f0e-82af-29a49adfb0e3)
