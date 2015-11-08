using System;

namespace Windtalker.Plumbing
{
    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}