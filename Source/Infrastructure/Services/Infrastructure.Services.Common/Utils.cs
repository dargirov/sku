using System;

namespace Infrastructure.Services.Common
{
    public static class Utils
    {
        public static int? TryParseInt(string str)
        {
            int result;
            if (!int.TryParse(str, out result))
            {
                return null;
            }

            return result;
        }

        public static bool? TryParseBool(string str)
        {
            bool result;
            if (!bool.TryParse(str, out result))
            {
                return null;
            }

            return result;
        }

        public static Guid? TryParseGuid(string str)
        {
            Guid result;
            if (!Guid.TryParse(str, out result))
            {
                return null;
            }

            return result;
        }
    }
}
