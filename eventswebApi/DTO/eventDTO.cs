namespace eventswebApi.DTO
{
    public class eventDTO
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxRegistrations { get; set; }
        public string Location { get; set; }
    }
}
