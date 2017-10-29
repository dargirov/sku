﻿using System;

namespace Infrastructure.Services.Common
{
    public static class Utils
    {
        public static int? TryParseInt(string str)
        {
            if (int.TryParse(str, out int result))
            {
                return result;
            }

            return null;
        }

        public static bool? TryParseBool(string str)
        {
            if (bool.TryParse(str, out bool result))
            {
                return result;
            }

            return null;
        }

        public static Guid? TryParseGuid(string str)
        {
            if (Guid.TryParse(str, out Guid result))
            {
                return result;
            }

            return null;
        }
    }
}
