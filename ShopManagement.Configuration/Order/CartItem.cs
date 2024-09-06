namespace ShopManagement.Configuration.Order;

public class CartItem
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double UnitPrice { get; set; }
    public string Picture { get; set; }
    public int Count { get; set; }
    public double TotalItemPrice { get; set; }

    public CartItem()
    {
        TotalItemPrice = UnitPrice * Count;
    }
}