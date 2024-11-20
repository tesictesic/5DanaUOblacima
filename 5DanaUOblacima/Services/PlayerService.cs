using _5DanaUOblacima.DTO.Get;
using _5DanaUOblacima.DTO.Post;
using DataAcess;
using Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace _5DanaUOblacima.Services
{
    public class PlayerService
    {
        private readonly GameContext _gameContext;
        public PlayerService(GameContext context)
        {
            _gameContext=context;
        }
        public bool AddPlayer(PlayerInsertDTO dto)
        {
            if (string.IsNullOrEmpty(dto.nickname)) return false;

            bool exist=_gameContext.Players.Any(y=>y.nickname == dto.nickname);
            if (exist) return false;

            Player player1 = new Player
            {
                nickname = dto.nickname,
            };
            _gameContext.Players.Add(player1);
            PlayerStatistic statistic = new PlayerStatistic
            {
                player_id = player1.id
            };
            _gameContext.PlayerStatistic.Add(statistic);
            _gameContext.SaveChanges();
            return true;
        }
       public async Task<List<PlayerGetDTO>> GetPlayer(Guid? id)
        {
            var query = _gameContext.Players.AsQueryable();
            if (id != null)
            {
                query = query.Where(y => y.id == id);
            }
            var result = query.Select(y => new PlayerGetDTO
            {
                id = y.id,
                nickname = y.nickname,
                wins = y.PlayerStatistic.Select(y => y.wins).First(),
                losses = y.PlayerStatistic.Select(y => y.losses).First(),
                elo = y.PlayerStatistic.Select(y => y.elo).First(),
                hoursPlayed = y.PlayerStatistic.Select(y => y.hoursPlayed).First(),
                team = y.TeamPlayer.Select(y => y.Team.teamName).FirstOrDefault()??null,
                ratingAdjustment = null
            }).ToList();


            return result;
        }
    }
}
