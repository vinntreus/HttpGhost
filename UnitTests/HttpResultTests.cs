using System.Linq;
using HttpGhost;
using HttpGhost.Navigation;
using HttpGhost.Transport;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class HttpResultTests
    {

        [Test]
        public void Find_NotFindingAnything_ReturnEmptyList()
        {
            var httpResult = BuildHttpResultWithResponseBody("");

            var result = httpResult.Find("//li");

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Find_FindingOneLiByXPath_ReturnListWithOneLi()
        {
            var httpResult = BuildHttpResultWithResponseBody("<li>bacon</li>");

            var result = httpResult.Find("//li");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.Raw, Is.EqualTo("<li>bacon</li>"));
            Assert.That(result.Text, Is.EqualTo("bacon"));
        }

        [Test]
        public void Find_FindingLiByClassByXPath_ReturnListWithOneLi()
        {
            var httpResult = BuildHttpResultWithResponseBody("<li class=\"flurp\">bacon</li><li>arne</li>");

            var result = httpResult.Find("//li[@class=\"flurp\"]");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.Raw, Is.EqualTo("<li class=\"flurp\">bacon</li>"));
        }

        [Test]
        public void Find_FindingMultipleLiByXPath_ReturnListWithOneLi()
        {
            var httpResult = BuildHttpResultWithResponseBody("<li class=\"flurp\">bacon</li><li>arne</li>");

            var result = httpResult.Find("//li");

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Find_FindingByClassDescendantsByXPath_ReturnListWithOneLi()
        {
            var httpResult = BuildHttpResultWithResponseBody("<li class=\"flurp\"><li class=\"tjong\">arne</li></li>");

            var result = httpResult.Find("//*[contains(@class,'flurp')]/*[contains(@class,'tjong')]");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.Raw, Is.EqualTo("<li class=\"tjong\">arne</li>"));
        }


        [Test]
        public void Find_FindingOneLiByCSS_ReturnListWithOneLi()
        {
            var httpResult = BuildHttpResultWithResponseBody("<li>bacon</li>");

            var result = httpResult.Find("li");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.Raw, Is.EqualTo("<li>bacon</li>"));
        }

        [Test]
        public void Find_FindingLiByClassByCSS_ReturnListWithOneLi()
        {
            var httpResult = BuildHttpResultWithResponseBody("<li class=\"flurp\">bacon</li><li>arne</li>");

            var result = httpResult.Find("li.flurp");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.Raw, Is.EqualTo("<li class=\"flurp\">bacon</li>"));
        }

        [Test]
        public void Find_FindingByClassDescendantsByCSS_ReturnListWithOneLi()
        {
            var httpResult = BuildHttpResultWithResponseBody("<li class=\"flurp\"><li class=\"tjong\">arne</li></li>");

            var result = httpResult.Find(".flurp .tjong");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.Raw, Is.EqualTo("<li class=\"tjong\">arne</li>"));
        }

        [Test]
        public void Follow_NoElements_ThrowsException()
        {
            var httpResult = BuildHttpResultWithResponseBody("");

            Assert.That(() => httpResult.Follow("#link"),
                Throws.TypeOf<NavigationResultException>().With.Message.StringContaining("Could not find element with href"));
        }

        [Test]
        public void FindForm_NoForm_ThrowsException()
        {
            var httpResult = BuildHttpResultWithResponseBody("");

            Assert.That(() => httpResult.FindForm("#form"),
                Throws.TypeOf<NavigationResultException>().With.Message.StringContaining("Could not find form"));
        }


        private static IHttpResult BuildHttpResultWithResponseBody(string body)
        {
            return new HttpResult(new NavigationResult(new Request(""), new Response { Body = body }));
        }
    }
}