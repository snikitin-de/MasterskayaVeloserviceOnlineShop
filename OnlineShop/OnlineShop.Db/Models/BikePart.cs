namespace OnlineShop.Db.Models
{
    public class BikePart : Product
    {
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Material { get; set; }
        public string? Color { get; set; }
        public float? Weight { get; set; }
        public float? Diameter { get; set; }
        public float? Width { get; set; }
        public float? Length { get; set; }
        public float? Depth { get; set; }
        public int? LinksNumber { get; set; }
        public float? InstallationWidth { get; set; }
        public float? InstallationDiameter { get; set; }
        public string? TypeAndPurpose { get; set; }
        public BikePartsCategories PartCategory { get; set; }
    }
}
