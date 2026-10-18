# Warehouse Management

A console-based warehouse management tool written in C#. Products are stored
in a CSV file and can be added, removed, restocked and listed — including a
dedicated view for items that have fallen below their minimum stock level.

The point of this project was not the feature list but the **structure**: it is
split into four layers with clear responsibilities, so that each part can be
changed without touching the others.

> The application interface is in German.

---

## Architecture

The project follows a layered architecture. Every layer only talks to the one
below it:

```
Presentation   → Program.cs              Menu, console I/O, input validation
Application    → ProductService.cs       Use cases, business rules
BuisnessLogic  → Product.cs              The Product entity and its invariants
DataAccess     → ProductFileManager.cs   Reading and writing the CSV file
```

What this buys you: the storage format is an implementation detail of
`ProductFileManager`. Swapping the CSV file for a database would mean rewriting
one class — the menu and the business rules would not change at all.

### Where the rules live

Validation is deliberately split across layers instead of sitting in the menu:

| Rule | Enforced in |
|---|---|
| Stock can never go below zero | `Product` (property setter) |
| No duplicate product name or number | `ProductService` |
| Minimum stock threshold | `Product._MinStock` |
| Input must be a valid number, name must not contain `;` | `Program` |

Because the `Stock` setter guards itself, an invalid value cannot be written
even if a future caller forgets to check first.

---

## Features

- **Add a product** — product number, name, stock and price, each with its own
  input validation loop that re-prompts until the value is valid
- **Remove a product** by product number
- **Increase or decrease stock** of a product by name
- **List all products** in an aligned table
- **List products below minimum stock** (default threshold: 5)

---

## Data storage

Products are persisted to `products.csv` next to the executable, one product
per line, semicolon-separated:

```
101; USB-Kabel; 50; 9.90
102; Netzteil 65W; 3; 25.50
103; Monitorhalterung; 12; 39.99
```

The file is created on first write. If it does not exist, the application
starts with an empty list instead of failing.

Product names may not contain a semicolon, since that is the field separator —
this is rejected at input time.

---

## Getting started

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/Colniro/LagerManagement
cd LagerManagement
dotnet run
```

---

## Usage

The application opens a menu and keeps running until you close the window:

```
================
Lager-Management
================

Was möchten Sie machen?
-------------------------

- Neues Produkt hinzufügen                                (1)
- Produkt entfernen                                       (2)
- Bestand eines Produktes erhöhen oder verringern         (3)
- Alle Produkte anzeigen                                  (4)
- Produkte mit unterschrittenem Mindestbestand anzeigen   (5)

Auswahl:
```

Pick a number and follow the prompts. Invalid input is caught and asked again
rather than crashing the program.

---

## Built with

- C# / .NET 10
- Nullable reference types and implicit usings enabled
- No external dependencies — standard library only

---

## Next steps

Things I would like to improve:

- Write the CSV with an invariant culture, so decimal prices stay readable on
  machines with a different regional setting
- Make the minimum stock configurable per product instead of one global value
- Add unit tests for `ProductService` — the layering already makes it testable,
  since the business rules do not depend on the console
