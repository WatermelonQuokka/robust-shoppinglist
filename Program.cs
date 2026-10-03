ShoppingList list = new ShoppingList("items.txt");
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
            isPriceValid = int.TryParse(Console.ReadLine(), out price) && price >= 0;
            if (!isPriceValid)
            {
                Console.Clear();
                Console.WriteLine("Du har skrivit in ett ogiltigt pris.\nSkriv in ett heltal som inte är negativt.\nFörsök igen genom att trycka på valfri tangent.");
                Console.ReadKey();
            }
        }
        while (!isPriceValid);
        list.Add(new Item(name, price));
    }
    else if (choice == 2)
    {
        if(list.Count == 0)
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
