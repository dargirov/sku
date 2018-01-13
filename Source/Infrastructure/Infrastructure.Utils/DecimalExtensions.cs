using System;

namespace Infrastructure.Utils
{
    public static class DecimalExtensions
    {
        public static decimal RoundFinance(this decimal value, int decimals)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }
    }
}
