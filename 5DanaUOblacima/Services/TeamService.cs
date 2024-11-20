 using _5DanaUOblacima.DTO.Get;
using _5DanaUOblacima.DTO.Post;
using DataAcess;
using Domain;

namespace _5DanaUOblacima.Services
{
    public class TeamService
    {
        private readonly GameContext _context;
        public TeamService(GameContext context)
        {
            this._context = context;
        }
        public bool AddTeam(TeamInsertDTO dto)
        {
            string teamName=dto.teamName;
            ICollection<Guid> teamPlayers = dto.players;
            if(teamPlayers.Count !=5) { return false; }

              var playersInOtherTeams = _context.TeamPlayers
                                         .Where(tp => teamPlayers.Contains(tp.playerId) && tp.Team.teamName != teamName)
                                         .Select(tp => tp.playerId)
                                         .ToList();

            if (playersInOtherTeams.Any()) { return false; }
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


            return true;
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
