using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace VideoGames.Application.Requests.VideoGame.Commands.DeleteVideoGame;

public class DeleteVideoGameCommand : IRequest
{
    public int Id { get; set; }
}
