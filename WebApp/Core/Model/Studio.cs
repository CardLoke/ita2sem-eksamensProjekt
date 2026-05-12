namespace Core.Model;

public class Studio
{
    public string StudioName { get; set; } = "";
    public int StudioSize { get; set; }
    public int StudioPrice { get; set; }
    public string StudioEquipment { get; set; } = "";
    public string StudioAddress { get; set; } = "";
    public string StudioDescription { get; set; } = "";
    public int Id { get; set; }
    public string Owner { get; set; } = "";
    public int Capacity { get; set; }
    
}