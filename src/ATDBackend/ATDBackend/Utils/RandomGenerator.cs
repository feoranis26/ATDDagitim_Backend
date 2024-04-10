namespace ATDBackend.Utils
{
    public static class RandomGenerator
    {
        [Flags]
        public enum RandomParts
        {
            Numbers = 1,
            Uppercase = 2,
            Lowercase = 4
        }

        private static Dictionary<RandomParts, string> map = new Dictionary<RandomParts, string>()
        {
            { RandomParts.Numbers, "1234567890" },
            { RandomParts.Uppercase, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
            { RandomParts.Lowercase, "abcdefghijklmnopqrstuvwxyz" }
        };

        public static string Generate(int length, RandomParts parts, params string[] extraChars)
        {
            string chars = string.Empty;

            for (int i = 0; i < extraChars.Length; i++) chars += extraChars[i];

            foreach(KeyValuePair<RandomParts, string> pair in map)
            {
                if ((parts & pair.Key) == pair.Key) chars += pair.Value;
            }

            if(chars.Length == 0) return string.Empty;

            string result = "";


            for(int i = 0; i < length; i++)
            {
                result += Random.Shared.Next(0, chars.Length);
            }

            return result;
        }
    }
}
