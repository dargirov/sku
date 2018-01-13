using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services.Common
{
    public class HashServices : IHashServices
    {
        private const string CharList = "0123456789abcdefghijklmnopqrstuvwxyz";

        // https://www.stum.de/2008/10/20/base36-encoderdecoder-in-c/
        public string Base36Encode(long input)
        {
            if (input < 0)
            {
                throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");
            }

            char[] clistarr = CharList.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(clistarr[input % 36]);
                input /= 36;
            }

            return new string(result.ToArray());
        }

        public long Base36Decode(string input)
        {
            var reversed = input.ToLower().Reverse();
            long result = 0;
            int pos = 0;

            foreach (char c in reversed)
            {
                result += CharList.IndexOf(c) * (long)Math.Pow(36, pos);
                pos++;
            }

            return result;
        }
    }
}
