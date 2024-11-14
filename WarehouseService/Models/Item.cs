namespace WarehouseService.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
    public string PictureUrl { get; set; }
    public int Quantity { get; set; }
}
