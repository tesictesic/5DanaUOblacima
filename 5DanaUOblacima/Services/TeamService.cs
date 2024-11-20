 using _5DanaUOblacima.DTO.Get;
using _5DanaUOblacima.DTO.Post;
using _5DanaUOblacima.Validations;
using DataAcess;
using Domain;
using FluentValidation;

namespace _5DanaUOblacima.Services
{
    public class TeamService
    {
        private readonly GameContext _context;
        private readonly TeamValidation _validation;
        public TeamService(GameContext context,TeamValidation validation)
        {
            this._context = context;
            this._validation = validation;
        }
        public void AddTeam(TeamInsertDTO dto)
        {
            _validation.ValidateAndThrow(dto);
            string teamName=dto.teamName;
            ICollection<Guid> teamPlayers = dto.players;
            Team team = new Team
            {
                teamName = teamName,
            };
            _context.Teams.Add(team);
            var teamPlayerEntities = teamPlayers.Select(playerId => new TeamPlayer
            {
                playerId = playerId,
                teamId = team.id
            }).ToList();

            _context.TeamPlayers.AddRange(teamPlayerEntities);
            _context.SaveChanges();


            
        }
        public async Task<List<TeamGetDTO>> GetTeams(Guid? id)
        {
            var query = _context.Teams.AsQueryable();
            if (id != null) query=query.Where(y=>y.id==id);

            var result = query.Select(y => new TeamGetDTO
            {
                id = y.id,
                teamName = y.teamName,
                players = (List<PlayerGetDTO>)y.TeamPlayers.Select(z => new PlayerGetDTO
                {
                    id = z.Player.id,
                    nickname = z.Player.nickname,
                    wins = z.Player.PlayerStatistic.Select(y => y.wins).First(),
                    losses = z.Player.PlayerStatistic.Select(y => y.losses).First(),
                    elo = z.Player.PlayerStatistic.Select(y => y.elo).First(),
                    hoursPlayed = z.Player.PlayerStatistic.Select(y => y.hoursPlayed).First(),
                    team = z.Player.TeamPlayer.Select(y => y.Team.teamName).FirstOrDefault() ?? null,
                    ratingAdjustment = 50
                })

            }).ToList();
            return result;
            
        }
    }
}
