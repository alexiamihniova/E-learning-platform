# E-learning Platform — Laborator 7

Implementarea a cinci paternuri comportamentale (Chain of Responsibility, State, Mediator, Template Method, Visitor) în contextul unei platforme de tip e-learning, dezvoltată în C# / .NET 8.

## Structura proiectului

```
E-learning platform/
├── Program.cs                              ← Demo pentru toate cele 5 paternuri
└── Patterns/
    ├── ChainOfResponsibility/              ← Sistem de suport tehnic
    ├── State/                              ← Ciclul de viață al unei înscrieri
    ├── Mediator/                           ← Sală de clasă virtuală
    ├── TemplateMethod/                     ← Generare rapoarte
    └── Visitor/                            ← Export conținut curs (JSON/XML/calcul timp)

E-learning platform.Tests/
└── PatternTests/                           ← Teste unitare xUnit pentru fiecare patern
```

## Rulare

```bash
# Build
dotnet build

# Demo (afișează în consolă rezultatul fiecărui patern)
dotnet run --project "E-learning platform"

# Teste unitare (xUnit)
dotnet test
```

## Paternurile implementate

| Patern | Domeniu | Beneficiu principal |
|---|---|---|
| Chain of Responsibility | Suport tehnic (FAQ → L1 → Plăți → Tehnic → Securitate) | Decuplare emitent/receptor, lanț extensibil |
| State | Înscriere la curs (Draft → PendingPayment → Active → InProgress → Completed) | Tranziții explicite, comportament localizat per stare |
| Mediator | Comunicare în sala de clasă virtuală | Reduce dependențele între participanți |
| Template Method | Rapoarte de curs (HTML / Text / SMS) | Reutilizare schelet algoritmic, personalizare pași |
| Visitor | Export conținut (JSON / XML / calcul timp) | Algoritmi noi fără modificarea structurii |
