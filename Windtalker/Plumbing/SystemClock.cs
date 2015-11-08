using System;
using Autofac;

namespace Windtalker.Plumbing
{
    [SingleInstance]
    public class SystemClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}