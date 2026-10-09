// TODO: låt användaren avbryta "Lägg till" och "Ta bort"?
int budget;
bool isBudgetValid;
do
{
    Console.Write("Vad vill du lägga för budget på shoppinglistan? ");
    isBudgetValid = int.TryParse(Console.ReadLine(), out budget) && budget >= 1;
    if (!isBudgetValid)
    {
        Console.Clear();
        Console.WriteLine("Du har skrivit in en ogiltig budget.\nSkriv in ett heltal som är större än 0.\nFörsök igen genom att trycka på valfri tangent.");
        Console.ReadKey();
    }
}
while (!isBudgetValid);
ShoppingList list = new ShoppingList("items.txt", budget);
list.Load();
string invalidChoice = "Skriv in ett giltigt alternativ.\nTryck på valfri tangent för att återgå till menyn.";

while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.Clear();
        Console.WriteLine(invalidChoice);
        Console.ReadKey();
        continue;
    }

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();
        int price;
        bool isPriceValid;
        do
        {
            Console.Write($"Pris för {name}: ");
            isPriceValid = Item.TryParsePrice(Console.ReadLine(), out price);
            if (!isPriceValid)
            {
                Console.Clear();
                Console.WriteLine("Du har skrivit in ett ogiltigt pris.\nSkriv in ett heltal som är större än 0.\nFörsök igen genom att trycka på valfri tangent.");
                Console.ReadKey();
            }
        }
        while (!isPriceValid);
        try
        {
            list.Add(new Item(name, price));
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.Clear();
            Console.WriteLine("Priset får inte vara 0 eller lägre.\nTryck på valfri tangent för att återgå till menyn.");
            Console.ReadKey();
        }
        catch (ArgumentException ex)
        {
            Console.Clear();
            Console.WriteLine($"Varan kunde inte läggas till.\n{ex.Message}\nTryck på valfri tangent för att återgå till menyn.");
            Console.ReadKey();
        }
        catch (InvalidOperationException ex)
        {
            Console.Clear();
            Console.WriteLine($"Varan kunde inte läggas till.\n{ex.Message}\nTryck på valfri tangent för att återgå till menyn.");
            Console.ReadKey();
        }
    }
    else if (choice == 2)
    {
        if (list.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Det finns ingen vara att ta bort.\nTryck på valfri tangent för att återgå till huvudmenyn.");
            Console.ReadKey();
            continue;
        }
        bool numberInInterval;
        int number;
        do
        {
            Console.Clear();
            list.Print();
            Console.Write("Nummer: ");
            numberInInterval = int.TryParse(Console.ReadLine(), out number) && number >= 1 && number <= list.Count;
            if (!numberInInterval)
            {
                Console.Clear();
                Console.WriteLine("Ditt alternativ finns inte i listan.\nVälj ett giltigt alternativ.\nFörsök igen genom att trycka på valfri tangent.");
                Console.ReadKey();
            }
        }
        while (!numberInInterval);
        list.RemoveAt(number);
    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
    else
    {
        Console.Clear();
        Console.WriteLine(invalidChoice);
        Console.ReadKey();
    }
}
