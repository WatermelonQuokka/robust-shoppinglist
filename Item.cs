// One item on the shopping list.
class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price)
    {
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
