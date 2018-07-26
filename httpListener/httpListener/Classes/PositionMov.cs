namespace httpListener.Classes
{
    class PositionMov:Father
    {
        public System.Guid Id { get; set; }

        public string FullName { get; set; }

        public long SalaryFrom { get; set; }

        public string Schedule { get; set; }

        public bool Trips { get; set; }

        public string About { get; set; }

        public double Rate { get; set; }

        public new System.DateTimeOffset DateOff { get; set; }

        public long SalaryTo { get; set; }

        public bool IsOwn { get; set; }
    }
}
