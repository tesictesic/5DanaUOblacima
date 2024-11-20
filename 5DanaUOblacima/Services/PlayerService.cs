using _5DanaUOblacima.DTO.Get;
using _5DanaUOblacima.DTO.Post;
using _5DanaUOblacima.Validations;
using DataAcess;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace _5DanaUOblacima.Services
{
    public class PlayerService
    {
        private readonly GameContext _gameContext;
        private readonly PlayerValidation _validation;
        public PlayerService(GameContext context,PlayerValidation validation)
        {
            _gameContext=context;
            _validation=validation;
        }
        public void AddPlayer(PlayerInsertDTO dto)
        {
           this._validation.ValidateAndThrow(dto);

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
