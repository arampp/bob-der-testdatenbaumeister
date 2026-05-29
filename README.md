# Bob der Testdatenbaumeister

Slides und Demos zum Talk **"Bob der Testdatenbaumeister – wartbare Tests trotz KI-Generierung"** von Alexander Rampp ([XITASO](https://xitaso.com)).

## Branches

| Branch     | Inhalt                              |
| ---------- | ----------------------------------- |
| `main`     | Ausgangszustand der Demos           |
| `solution` | Live-Coding-Ergebnisse aus dem Talk |

## Demos

Jede Demo ist eine eigenständige .NET-Solution unter `demos/`:

| #   | Ordner                          | Thema                             |
| --- | ------------------------------- | --------------------------------- |
| 1   | `demos/1_intro`                 | Einführung in Test Data Builder   |
| 2   | `demos/2_hierarchische_builder` | Hierarchische Builder             |
| 3   | `demos/3_bogus`                 | Testdaten mit Bogus               |
| 4   | `demos/4_ki`                    | KI-gestützte Testdatengenerierung |
| 5   | `demos/5_dsl`                   | DSL für Testdaten                 |

## Voraussetzungen

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Ausführen

```bash
cd demos/<demo-ordner>
dotnet test
```

## Kontakt

Alexander Rampp freut sich über Vernetzung via [LinkedIn](https://www.linkedin.com/in/arampp/).
