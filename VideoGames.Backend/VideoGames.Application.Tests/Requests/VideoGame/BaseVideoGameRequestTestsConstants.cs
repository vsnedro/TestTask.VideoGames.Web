using System;
using System.Collections.Generic;

namespace VideoGames.Application.Tests.Requests.VideoGame;

public static class BaseVideoGameRequestTestsConstants
{
    /// <summary>
    /// Video game constants for testing.
    /// </summary>
    public static class VideoGames
    {
        public static class A
        {
            public const int Id = 1;
            public const string Name = "VideoGame A";
            public static readonly DateTime ReleaseDate = DateTime.Parse("01/01/2001");
            public const int DeveloperStudioId = BaseRequestTestsConstants.DeveloperStudios.A.Id;
            public const string DeveloperStudioName = BaseRequestTestsConstants.DeveloperStudios.A.Name;
            public static readonly IEnumerable<int> GenreIds = new List<int>
            {
                BaseRequestTestsConstants.VideoGameGenres.A.Id,
            };
        }

        public static class B
        {
            public const int Id = 2;
            public const string Name = "VideoGame B";
            public static readonly DateTime ReleaseDate = DateTime.Parse("02/02/2002");
            public const int DeveloperStudioId = BaseRequestTestsConstants.DeveloperStudios.B.Id;
            public const string DeveloperStudioName = BaseRequestTestsConstants.DeveloperStudios.B.Name;
            public static readonly IEnumerable<int> GenreIds = new List<int>
            {
                BaseRequestTestsConstants.VideoGameGenres.A.Id,
                BaseRequestTestsConstants.VideoGameGenres.B.Id,
            };
        }

        public static class C
        {
            public const int Id = 3;
            public const string Name = "VideoGame C";
            public static readonly DateTime ReleaseDate = DateTime.Parse("03/03/2003");
            public const int DeveloperStudioId = BaseRequestTestsConstants.DeveloperStudios.C.Id;
            public const string DeveloperStudioName = BaseRequestTestsConstants.DeveloperStudios.C.Name;
            public static readonly IEnumerable<int> GenreIds = new List<int>
            {
                BaseRequestTestsConstants.VideoGameGenres.A.Id,
                BaseRequestTestsConstants.VideoGameGenres.B.Id,
                BaseRequestTestsConstants.VideoGameGenres.C.Id,
            };
        }
    }

    public const int WrongVideoGameId = 666;
}
