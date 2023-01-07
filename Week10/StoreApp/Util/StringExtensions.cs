namespace StoreApp.Util
{
    public static class StringExtensions
    {
        public static int? ParseIntOrDefault(this string text) =>
            int.TryParse(text, out var v) ? (int?)v : null;
    }
}
