using _5DanaUOblacima.DTO.Post;
using DataAcess;
using FluentValidation;
namespace _5DanaUOblacima.Validations
{
    public class PlayerValidation:AbstractValidator<PlayerInsertDTO>
    {
        private readonly GameContext _gameContext;
        public PlayerValidation(GameContext gameContext)
        {
            _gameContext = gameContext;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.nickname).NotEmpty()
                                  .WithMessage("Player nickname cannot be empty")
                                  .Must(nickname => !_gameContext.Players.Any(z => z.nickname == nickname))
                                  .WithMessage("Player nickname must be unique");
        }
    }
}
