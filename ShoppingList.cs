// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    public int Count => items.Count;
    private string path;
    private int limit;

    public ShoppingList(string path, int limit)
    {
        this.path = path;
        this.limit = limit;
    }

    public void Add(Item item)
    {
        if(Total() + item.Price > limit)
        {
            throw new InvalidOperationException($"Varan kostar {item.Price} kr men du har bara {limit - Total()} kr kvar av din budget.");
        }
        items.Add(item);
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
        }
        catch (UnauthorizedAccessException)
        {
            Console.Clear();
            Console.WriteLine("!OBS!\nProgrammet har inte tillåtelse att spara dina ändringar i listan.\nDina ändringar finns fortfarande i programmet men förloras om du avslutar.\nKontrollera att filen inte är skrivskyddad och välj Spara igen i menyn.\nAnnars be någon kunnig om hjälp eller skriv av listan som visas ovanför menyn innan avslut.\nTryck på valfri tangent för att återgå till menyn.");
            Console.ReadKey();
            return;
        }
        catch (IOException)
        {
            Console.Clear();
            Console.WriteLine("!OBS!\nListan kunde inte sparas.\nDina ändringar finns fortfarande i programmet men förloras om du avslutar.\nProva att stänga ner andra program som kanske använder filen och spara igen.\nTryck på valfri tangent för att återgå till menyn.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Listan är sparad.");
    }

    // Reads the file back into the list.
    public void Load()
    {
        string text;
        try
        {
            text = File.ReadAllText(path);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Shoppinglistan kunde inte hittas.\nIfall du är ny användare välj Spara i menyn för att skapa en ny shoppinglista.");
            return;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Programmet har inte tillåtelse att öppna din shoppinglista.\nSpara inte om du vill behålla din sparade shoppinglista.\nTesta att flytta programmet till en egen mapp eller be någon kunnig om hjälp.");
            return;
        }
        catch (IOException)
        {
            Console.WriteLine("Shoppinglistan kunde inte öppnas just nu.\nSpara inte om du vill behålla din sparade shoppinglista.\nProva att stänga ner andra program och starta om programmet för att försöka igen.");
            return;
        }
        string[] lines = text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            string[] parts = line.Split(';');
            int price;
            if (parts.Length != 2 || !Item.TryParsePrice(parts[0], out price) || string.IsNullOrWhiteSpace(parts[1]))
            {
                // was planning to calculate the amount of rows skipped and showing them
                continue;
            }
            items.Add(new Item(parts[1].Trim(), price));
        }
    }
}
