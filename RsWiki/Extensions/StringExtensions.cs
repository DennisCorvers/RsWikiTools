namespace RsWiki.Extensions
{
    public static class StringExtensions
    {
        public static string CapitaliseFirstLetter(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var chars = str.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return new string(chars);
        }
    }
}
