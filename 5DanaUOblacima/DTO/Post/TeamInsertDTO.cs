namespace _5DanaUOblacima.DTO.Post
{
    public class TeamInsertDTO
    {
        public string teamName { get; set; }
        public ICollection<Guid> players { get; set; }
    }
}
