namespace Mearcury.Azure
{
    public static class Extensions
    {
        public static string Limit(this string source, int length)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            return source.Length < length ? source : source.Substring(0, length);
        }

        public static string Ellipsis(this string source, int length)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            return source.Length < length ? source : (source.Substring(0, length - 3) + "...");
        }
    }
}
