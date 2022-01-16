using Microsoft.AspNetCore.Mvc;

using VideoGames.Application.Requests.VideoGame.Commands.CreateVideoGame;
using VideoGames.Application.Requests.VideoGame.Commands.DeleteVideoGame;
using VideoGames.Application.Requests.VideoGame.Commands.UpdateVideoGame;
using VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameDetails;
using VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameList;
using VideoGames.WebApi.Models.VideoGame;

namespace VideoGames.WebApi.Controllers.VideoGame;

/// <summary>
/// Осуществляет взаимодействие с базой данных, в которой хранятся данные о видеоиграх.
/// </summary>
[Produces("application/json")]
public class VideoGameController : ApiBaseController
{
    /// <summary>
    /// Получить данные о видеоигре по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор видеоигры.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VideoGameListVm>> Get(int id, CancellationToken cancellationToken)
    {
        var query = new GetVideoGameDetailsQuery()
        {
            Id = id,
        };
        var vm = await Mediator.Send(query, cancellationToken);

        return Ok(vm);
    }

    /// <summary>
    /// Получить данные о всех видеоиграх.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VideoGameListVm>> GetAll([FromQuery] GetVideoGameListDto getVideoGameListDto, CancellationToken cancellationToken)
    {
        var query = new GetVideoGameListQuery()
        {
            PageIndex = getVideoGameListDto.PageIndex,
            PageSize = getVideoGameListDto.PageSize,
            ReleaseYear = getVideoGameListDto.ReleaseYear,
            DeveloperStudioId = getVideoGameListDto.DeveloperStudioId,
            VideoGameGenreId = getVideoGameListDto.VideoGameGenreId,
        };
        var vm = await Mediator.Send(query, cancellationToken);

        return Ok(vm);
    }

    /// <summary>
    /// Добавить данные о видеоигре.
    /// </summary>
    /// <param name="createVideoGameDto">Данные для добавления видеоигры.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create([FromBody] CreateVideoGameDto createVideoGameDto, CancellationToken cancellationToken)
    {
        var command = new CreateVideoGameCommand()
        {
            Name = createVideoGameDto.Name,
            ReleaseDate = createVideoGameDto.ReleaseDate,
            DeveloperStudioId = createVideoGameDto.DeveloperStudioId,
            GenresId = createVideoGameDto.GenresId.ToList(),
        };
        var noteId = await Mediator.Send(command, cancellationToken);

        return Ok(noteId);
    }

    /// <summary>
    /// Обновить данные о видеоигре.
    /// </summary>
    /// <param name="updateVideoGameDto">Данные для обновления видеоигры.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update([FromBody] UpdateVideoGameDto updateVideoGameDto, CancellationToken cancellationToken)
    {
        var command = new UpdateVideoGameCommand()
        {
            Id = updateVideoGameDto.Id,
            Name = updateVideoGameDto.Name,
            ReleaseDate = updateVideoGameDto.ReleaseDate,
            DeveloperStudioId = updateVideoGameDto.DeveloperStudioId,
            GenresId = updateVideoGameDto.GenresId.ToList(),
        };
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Удалить данные о видеоигре.
    /// </summary>
    /// <param name="id">Идентификатор видеоигры.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteVideoGameCommand()
        {
            Id = id,
        };
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
