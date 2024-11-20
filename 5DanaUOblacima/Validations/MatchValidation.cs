using _5DanaUOblacima.DTO.Post;
using DataAcess;
using FluentValidation;
namespace _5DanaUOblacima.Validations
{
    public class MatchValidation:AbstractValidator<MatchInsertDTO>
    {
        private readonly GameContext _gameContext;
        public MatchValidation(GameContext context)
        {
            this._gameContext = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.team1Id).NotEmpty()
                                 .WithMessage("Team 1 cannot be empty")
                                 .Must(team1Id => _gameContext.Teams.Any(y => y.id == team1Id))
                                 .WithMessage("Team 1 is not exist")
                                 .Must((dto, team1Id) => dto.team2Id != dto.team1Id)
                                 .WithMessage("Team 1 and Team 2 cannot be same");

            RuleFor(x => x.team2Id).NotEmpty()
                                 .WithMessage("Team 1 cannot be empty")
                                 .Must(team1Id => _gameContext.Teams.Any(y => y.id == team1Id))
                                 .WithMessage("Team 1 is not exist")
                                 .Must((dto, team2Id) => dto.team1Id != dto.team2Id)
                                 .WithMessage("Team 2 and Team 1 cannot be same");

            RuleFor(x => x.winningTeamId).Must(winningTeamId => winningTeamId == null || _gameContext.Teams.Any(y => y.id == winningTeamId))
                                         .WithMessage("Winning Team must be null or exist in the Teams table.");



        }
    }
}
