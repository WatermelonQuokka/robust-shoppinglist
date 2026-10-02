# Robust Shopping list
## Felrapport
### Fel nr 1: Programkrasch vid uppstart på grund av tom rad i slutet av filen
- **Var:** `Load()`, rad 90 i originalkoden `items.Add(new Item(parts[1], int.Parse(parts[0])));`
- **Symptom:** Programkrasch vid uppstart, IndexOutOfRangeException
- **Orsak:** `Save()` skriver `\r\n` efter varje vara (item) så att det slutar med en radbrytning. Eftersom `Split('\n')` i `Load()` bryter vid varje `\n` så returneras även det som kommer efter den sista `\n` vilket i detta fallet blir en tom sträng `""`. Nästkommande `Split(';')` returnerar en array med ett element, en tom sträng `""` för att det inte finns någon `;` att bryta vid. Rad 90 kraschar för att den försöker läsa `parts[1]` men arrayen har bara ett element på index 0, vilket då innebär att index 1 inte existerar och ger *IndexOutOfRangeException*
- **Lösning:** Min lösning för detta var att sätta in ett villkor ifall strängen `line` innehåller null, blanktecken eller är tom och isåfall skippa resten av iterationen och gå till nästa rad i loopen. Detta fungerar eftersom den tomma strängen nu aldrig når `line.Split(';')` så en parts array blir inte skapad för detta. Jag valde att lösa problemet i `Load()` istället för `Save()` eftersom jag tycker att `Load()` inte ska lita på att filen är perfekt. 
### Fel nr 2: `\r` ligger kvar i strängar
- **Var:** Rad 85 `Split('\n')` i `Load()` lämnar kvar `\r` i varje rad och rad 90 i originalkoden använder namnet med `\r` i sig.
- **Symptom:** Vid uppstart så printas listan ut och jag upptäckte att namn på varor inte finns utan endast pris. Dock så såg jag genom debuggern att namnen finns kvar i datan men ser ut som att de inte finns kvar när det printas ut.
- **Orsak:** `Save()` skriver `\r\n` men `Load()` använder bara `Split('\n')`. Detta innebär att namnen fortfarande innehåller t.ex. `Mjölk\r` istället för `Mjölk` och namnen försvinner eftersom `\r` innebär att gå till början av raden vilket då gör att priset skriver över namnet på varan (item).
- **Lösning:** Jag la till `.Trim()` på `parts[1]` vilket helt enkelt tar bort blanktecken i början och slutet av en sträng som i detta fall är då `\r`. Det krävdes inte på `parts[0]` vilket är priset då `int.Parse` tar hand om blanktecken redan.

## Designval

## Klassdiagram