namespace httpListener.Classes
{
    class ExperienceMov:Father
    {
        public System.Guid Id { get; set; }

        public string CompanyName { get; set; }

        public string Position { get; set; }

        public System.Guid ProfileId { get; set; }

        public System.DateTimeOffset FromDate { get; set; }

        public System.DateTimeOffset ToDate { get; set; }

        public string About { get; set; }

        public string City { get; set; }

        public new System.DateTimeOffset DateOff { get; set; }
    }
}
