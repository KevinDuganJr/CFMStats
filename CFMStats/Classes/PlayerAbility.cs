namespace CFMStats.Classes
{
    public class PlayerAbility
    {
        public string ActivationDescription { get; set; }

        public string DeactivationDescription { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public bool IsEmpty { get; set; }

        public bool IsLocked { get; set; }

        public int OvrThreshold { get; set; }

        public string Title { get; set; }
    }
}