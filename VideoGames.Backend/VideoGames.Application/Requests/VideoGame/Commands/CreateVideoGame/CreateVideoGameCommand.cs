using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace VideoGames.Application.Requests.VideoGame.Commands.CreateVideoGame;

public class CreateVideoGameCommand : IRequest<int>
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int DeveloperStudioId { get; set; }
    public IEnumerable<int> GenresId { get; set; } = new List<int>();
}
