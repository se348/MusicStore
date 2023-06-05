namespace MusicStore.Helpers
{
    public class MyHelper
    {
        public static string Truncate(string text, int maxLength)
        {
            if (text.Length <= maxLength) return text;

            return text.Substring(0, maxLength) + "...";
        }
    }
}
