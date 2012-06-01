namespace Okazuki.TwitterSearch.Model
{
    public static class StringUtils
    {
        public static string Decode(string str)
        {
            return str.Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&");
        }
    }
}
