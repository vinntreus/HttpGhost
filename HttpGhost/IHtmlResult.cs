using HttpGhost.Html;

namespace HttpGhost
{
    public interface IHtmlResult : IHttpResult
    {
        /// <summary>
        /// Finds html-elements by using css-selector or xpath
        /// </summary>
        /// <param name="selector">Css or xpath-selector</param>
        /// <returns>Html elements collection</returns>
        Elements Find(string selector);

        /// <summary>
        /// Find html form element by using css-selector or xpath
        /// </summary>
        /// <param name="selector">Css or xpath-selector</param>
        /// <returns>Form element</returns>
        Form FindForm(string selector);

        /// <summary>
        /// Follow a link, aka simulate a click on a href-element, use css- or xpath-selector to find link and make get request from its href attribute
        /// </summary>
        /// <param name="selector">Css or xpath-selector</param>
        /// <returns>Httpresult</returns>
        IHtmlResult Follow(string selector);
    }
}