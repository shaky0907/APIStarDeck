using System.Text;
using System;

namespace StarDeckAPI.Utilities
{
    public static class GeneratorID
    {
        private static Random random = new Random();
        public static string GenerateRandomId(string prefix)
        {
            const int idLength = 14;
            StringBuilder sb = new StringBuilder(idLength);
            sb.Append(prefix);

            while (sb.Length < idLength)
            {
                char c = (char)random.Next('0', 'z' + 1);
                if (Char.IsLetterOrDigit(c) && sb[sb.Length - 1] != c)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
