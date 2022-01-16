using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string name, object key)
        : base(FormatMessage(name, key))
    { }

    public EntityNotFoundException(string name, object key, Exception innerException)
        : base(FormatMessage(name, key), innerException)
    { }

    public EntityNotFoundException(string message)
        : base(message)
    { }

    public EntityNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    { }

    private static string FormatMessage(string name, object key) =>
        $"Entity [{name}] ({key}) not found.";
}
