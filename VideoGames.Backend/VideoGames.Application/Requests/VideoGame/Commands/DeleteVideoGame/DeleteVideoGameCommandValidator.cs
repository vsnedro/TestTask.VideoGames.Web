using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace VideoGames.Application.Requests.VideoGame.Commands.DeleteVideoGame;

public class DeleteVideoGameCommandValidator : AbstractValidator<DeleteVideoGameCommand>
{
    public DeleteVideoGameCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
