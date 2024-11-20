using _5DanaUOblacima.DTO.Post;
using _5DanaUOblacima.Validations;
using DataAcess;
using Domain;
using FluentValidation;

namespace _5DanaUOblacima.Services
{
    public class MatchService
    {
        private readonly GameContext _gameContext;
        private readonly MatchValidation _validation;
        public MatchService(GameContext context, MatchValidation validation)
        {
            this._gameContext = context;
            this._validation = validation;
        }
        public void AddMatch(MatchInsertDTO dto)

        {


            this._validation.ValidateAndThrow(dto);

            Matches match = new Matches
            {
                team1Id = dto.team1Id,
                team2Id = dto.team2Id,
                winingTeamId = dto.winningTeamId,
                duration = dto.duration
            };
            _gameContext.Matches.Add(match);
            _gameContext.SaveChanges();
            this.GetTeam(dto);
        }
        private void GetTeam(MatchInsertDTO dto)
        {
            Guid? winingTeamId = null;
            Guid? losingTeamId = null;
            bool draw = false;

            if (dto.team1Id == dto.winningTeamId)
            {
                winingTeamId = dto.team1Id;
                losingTeamId = dto.team2Id;
                draw = false;
            }
            else if (dto.team2Id == dto.winningTeamId)
            {
                winingTeamId = dto.team2Id;
                losingTeamId = dto.team1Id;
                draw = false;
            }
            else if (dto.winningTeamId == null) {
                draw = true;
                winingTeamId = dto.team1Id;
                losingTeamId = dto.team2Id;
            } 
            Team team1 = _gameContext.Teams.Find(winingTeamId);
            Team team2 = _gameContext.Teams.Find(losingTeamId);
            int duration = dto.duration;
            this.InsertPlayerStatistic(team1, team2, duration, draw);

        }
        private void InsertPlayerStatistic(Team team1,Team team2, int duration,bool draw = false)
        {
            var team1Players = team1.TeamPlayers.Select(y => y.Player).ToList();
            var team2Players = team2.TeamPlayers.Select(y => y.Player).ToList(); 
            foreach(var player in team1Players)
            {
               double S = 1;
                if (draw)
                {
                    S = 0.5;
                }
                if (S == 1) {

                    player.PlayerStatistic
                    .ToList()
                    .ForEach(stat => stat.wins += 1);
                    _gameContext.SaveChanges();

                } 
                this.UpdatePlayerStatistic(player, S, duration, team2Players);    
            }
            foreach(var player in team2Players)
            {
                double S  = 0;
                if (draw)
                {
                    S = 0.5;
                }
                if (S == 0)
                {
                            player.PlayerStatistic
                  .ToList()
                  .ForEach(stat => stat.losses += 1);
                    _gameContext.SaveChanges();
                }
                this.UpdatePlayerStatistic(player, S, duration, team1Players);
            }

        }
        private void UpdatePlayerStatistic(Player player,double S,int duration, List<Player>teamPlayerOpponent) 
        {
           var OpponentStatistic=teamPlayerOpponent.Select(y=>y.PlayerStatistic).ToList();

            double R2 = OpponentStatistic.Sum(y => y.Average(z => z.elo));
            double R1 = player.PlayerStatistic.Select(y => y.elo).First();

            double exoectedElo = CalculateExpectedElo(R1, R2);

            int hoursPlayed = player.PlayerStatistic.Select(y => y.hoursPlayed).First() + duration;
            int K = GetRatingAdjustment(hoursPlayed);
            double newElo=CalculateNewElo(R1,exoectedElo,S,K);

            var playerElo=player.PlayerStatistic.Select(y => y.elo).First();
                player.PlayerStatistic
        .ToList() 
        .ForEach(stat => stat.hoursPlayed += hoursPlayed);
            player.PlayerStatistic.ToList().ForEach(stat => stat.elo = newElo);
            _gameContext.SaveChanges();


        }
        private double CalculateExpectedElo(double R1, double R2)
        {
            return 1 / (1 + Math.Pow(10, (R2 - R1) / 400));
        }

        private double CalculateNewElo(double R1, double expectedElo, double actualScore, int K)
        {
            return R1 + K * (actualScore - expectedElo);
        }

        private int GetRatingAdjustment(int hoursPlayed)
        {
            if (hoursPlayed < 500) return 50;
            if (hoursPlayed < 1000) return 40;
            if (hoursPlayed < 3000) return 30;
            if (hoursPlayed < 5000) return 20;
            return 10;
        }
    }
}
