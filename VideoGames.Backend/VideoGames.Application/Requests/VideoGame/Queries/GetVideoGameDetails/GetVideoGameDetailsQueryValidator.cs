using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameDetails;

public class GetVideoGameDetailsQueryValidator : AbstractValidator<GetVideoGameDetailsQuery>
{
    public GetVideoGameDetailsQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
