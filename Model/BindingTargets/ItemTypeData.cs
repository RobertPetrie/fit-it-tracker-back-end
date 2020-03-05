namespace fix_it_tracker_back_end.Model.BindingTargets
{
    public class ItemTypeData
    {
        public string Name { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public ItemType ItemType => new ItemType
        {
            Name = Name,
            Model = Model,
            Manufacturer = Manufacturer
        };
    }
}
