namespace fix_it_tracker_back_end.Model.BindingTargets
{
    public class FaultData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Fault Fault => new Fault
        {
            Name = Name,
            Description = Description
        };
    }
}
