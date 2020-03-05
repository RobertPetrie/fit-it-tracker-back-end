namespace fix_it_tracker_back_end.Model.BindingTargets
{
    public class ResolutionData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Resolution Resolution => new Resolution
        {
            Name = Name,
            Description = Description
        };

    }
}
