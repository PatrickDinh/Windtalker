using System;

namespace Windtalker.Plumbing
{
    public interface IClock
    {
        DateTimeOffset UtcNow { get; }
    }
}