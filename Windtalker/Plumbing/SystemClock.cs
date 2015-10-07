using System;
using Autofac;

namespace Windtalker.Plumbing
{
    [SingleInstance]
    public class SystemClock : IClock
    {
        public DateTimeOffset UtcNow
        {
            get { return DateTimeOffset.UtcNow; }
        }
    }
}