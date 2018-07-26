namespace httpListener.Classes
{
    class ProfileToPositionMov:Father
    {
        public System.Guid Id { get; set; }
        public System.Guid ProfileId { get; set; }
        public System.Guid PositionId { get; set; }
        public new System.DateTimeOffset DateOff { get; set; }
    }
}
