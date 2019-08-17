namespace Warehouse.Web.Infrastructure
{
    public static class HtmlBuilder
    {
        public static string BuildTitleDescription(string innerText)
        {
            string result = "<p class=\"text-white pt-4 pb-4\">" +
                                innerText + 
                            "</p>";

            return result;
        }
    }
}
