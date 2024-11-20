namespace _5DanaUOblacima.DTO.Get
{
    public class PlayerGetDTO:BaseDTO
    {
        public string nickname { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public double elo { get; set; }
        public int hoursPlayed { get; set; }
        public string team { get; set; }
        public int? ratingAdjustment { get; set; }
    }
}
