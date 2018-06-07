using System;

namespace Infrastructure.Services.Common
{
    // TODO: use interface to make it testable
    public static class Time
    {
        public static DateTime Now => DateTime.Now;
        public static DateTime Today => DateTime.Today;
    }
}
