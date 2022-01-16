using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameDetails;

public class GetVideoGameDetailsQuery : IRequest<VideoGameDetailsVm>
{
    public int Id { get; set; }
}
