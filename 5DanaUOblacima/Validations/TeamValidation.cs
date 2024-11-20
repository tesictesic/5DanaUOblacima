using _5DanaUOblacima.DTO.Post;
using DataAcess;
using FluentValidation;
namespace _5DanaUOblacima.Validations;

public class TeamValidation:AbstractValidator<TeamInsertDTO>
{
    private readonly GameContext gameContext;
    public TeamValidation(GameContext context)
    {
        this.gameContext = context;

        RuleFor(x=>x.teamName).NotEmpty()
            .WithMessage("Team name cannot be empty")
            .Must(teamName => !gameContext.Teams.Any(t => t.teamName == teamName))
            .WithMessage("Team name must be unique");

        RuleFor(x=>x.players).NotEmpty()
            .WithMessage("A team must have players")
            .Must(players => players.Count == 5)
            .WithMessage("A team must have exactly 5 players");

        RuleForEach(x => x.players)
            .Must(playerId => gameContext.Players.Any(p => p.id == playerId))
            .WithMessage("One or more players are not found in the database")
            .Must(playerId => !gameContext.TeamPlayers.Any(tp => tp.playerId == playerId))
            .WithMessage("A player can belong to only one team and cannot switch teams");
    }
}
