namespace _5DanaUOblacima.DTO.Post
{
    public class MatchInsertDTO
    {
        public Guid team1Id { get; set; }
        public Guid team2Id { get; set;}
        public Guid? winningTeamId { get; set; }
        public int duration { get; set; }
    }
}
