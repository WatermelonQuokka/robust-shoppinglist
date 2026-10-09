// One item on the shopping list.
class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Namnet på varan får inte vara tom.");
        }
        if (price < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Priset får inte vara 0 eller lägre.");
        }
        Name = name;
        Price = price;
    }

    public static bool TryParsePrice(string text, out int price)
    {
        return int.TryParse(text, out price) && price >= 1;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
