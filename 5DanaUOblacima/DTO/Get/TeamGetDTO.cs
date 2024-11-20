namespace _5DanaUOblacima.DTO.Get
{
    public class TeamGetDTO:BaseDTO
    {
        public string teamName { get; set; }
        public List<PlayerGetDTO> players { get; set; }
    }
}
