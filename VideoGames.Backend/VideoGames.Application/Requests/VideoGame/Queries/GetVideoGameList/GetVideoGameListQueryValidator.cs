using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameList;

public class GetVideoGameListQueryValidator : AbstractValidator<GetVideoGameListQuery>
{
    public GetVideoGameListQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}
