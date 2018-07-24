namespace HRClient
{
    public class Experience
    {
        public System.Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public System.Guid ProfileId { get; set; }
        public System.DateTimeOffset FromDate { get; set; }
        public System.DateTimeOffset ToDate { get; set; }
        public string About { get; set; }
        public string City { get; set; }
        public System.DateTimeOffset DateOff { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
