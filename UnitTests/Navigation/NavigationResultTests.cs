using System.Linq;
using HttpGhost.Navigation;
using NUnit.Framework;

namespace UnitTests.Navigation
{
    [TestFixture]
    public class NavigationResultTests
    {
        private NavigationResult navigationResult;
        private FakeRequest requestFake;
        private FakeResponse responseFake;

        [SetUp]
        public void Setup()
        {
            requestFake = new FakeRequest();
            responseFake = new FakeResponse(); 
            navigationResult = new NavigationResult(requestFake, responseFake);
        }

        [Test]
        public void Find_NotFindingAnything_ReturnEmptyList()
        {
            responseFake.Html = "";

            var result = navigationResult.Find("//li");

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Find_FindingOneLiByXPath_ReturnListWithOneLi()
        {
            responseFake.Html = "<li>bacon</li>";

            var result = navigationResult.Find("//li");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0), Is.EqualTo("<li>bacon</li>"));
        }

        [Test]
        public void Find_FindingLiByClassByXPath_ReturnListWithOneLi()
        {
            responseFake.Html = "<li class=\"flurp\">bacon</li><li>arne</li>";

            var result = navigationResult.Find("//li[@class=\"flurp\"]");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0), Is.EqualTo("<li class=\"flurp\">bacon</li>"));
        }

        [Test]
        public void Find_FindingMultipleLiByXPath_ReturnListWithOneLi()
        {
            responseFake.Html = "<li class=\"flurp\">bacon</li><li>arne</li>";

            var result = navigationResult.Find("//li");

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Find_FindingByClassDescendantsByXPath_ReturnListWithOneLi()
        {
            responseFake.Html = "<li class=\"flurp\"><li class=\"tjong\">arne</li></li>";

            var result = navigationResult.Find("//*[contains(@class,'flurp')]/*[contains(@class,'tjong')]");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0), Is.EqualTo("<li class=\"tjong\">arne</li>"));
        }


        [Test]
        public void Find_FindingOneLiByCSS_ReturnListWithOneLi()
        {
            responseFake.Html = "<li>bacon</li>";

            var result = navigationResult.Find("li");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0), Is.EqualTo("<li>bacon</li>"));
        }

        [Test]
        public void Find_FindingLiByClassByCSS_ReturnListWithOneLi()
        {
            responseFake.Html = "<li class=\"flurp\">bacon</li><li>arne</li>";

            var result = navigationResult.Find("li.flurp");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0), Is.EqualTo("<li class=\"flurp\">bacon</li>"));
        }

        [Test]
        public void Find_FindingByClassDescendantsByCSS_ReturnListWithOneLi()
        {
            responseFake.Html = "<li class=\"flurp\"><li class=\"tjong\">arne</li></li>";

            var result = navigationResult.Find(".flurp .tjong");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0), Is.EqualTo("<li class=\"tjong\">arne</li>"));
        }
        
    }
}