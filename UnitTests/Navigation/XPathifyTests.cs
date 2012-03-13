using HttpGhost.Navigation.Implementation;
using HttpGhost.Navigation.Parse;
using NUnit.Framework;

namespace UnitTests.Navigation
{
    [TestFixture]
    public class XPathifyTests
    {
        private string GetXPathifyString(string pattern)
        {
            return new CssToXpath(pattern).Parse();
        }

        [Test]
        public void Create_StartsWithTwoForwardSlash_DoNothing()
        {
            var xpath = GetXPathifyString("//a");

            Assert.That(xpath, Is.EqualTo("//a"));
        }

        [Test]
        public void Create_StartsWithNoForwardSlash_AddTwoForwardSlash()
        {
            var xpath = GetXPathifyString("a");

            Assert.That(xpath, Is.EqualTo("//a"));
        }

        [Test]
        public void Create_ContainDot_AddClassAttribute()
        {
            var xpath = GetXPathifyString("a.beer");

            Assert.That(xpath, Is.EqualTo("//a[contains(@class,'beer')]"));
        }

        [Test]
        public void Create_ContainSharp_AddIdAttribute()
        {
            var xpath = GetXPathifyString("a#beer");

            Assert.That(xpath, Is.EqualTo("//a[@id=\"beer\"]"));
        }

        [Test]
        public void Create_ContainDescendent_AddSlash()
        {
            var xpath = GetXPathifyString("a#beer > li");

            Assert.That(xpath, Is.EqualTo("//a[@id=\"beer\"]/li"));
        }

        [Test]
        public void Create_ContainDescendentWithClass_AddSlash()
        {
            var xpath = GetXPathifyString("a#beer > li.carlsberg");

            Assert.That(xpath, Is.EqualTo("//a[@id=\"beer\"]/li[contains(@class,'carlsberg')]"));
        }

        [Test]
        public void Create_IdWithoutElement_AddStar()
        {
            var xpath = GetXPathifyString("#beer");

            Assert.That(xpath, Is.EqualTo("//*[@id=\"beer\"]"));
        }

        [Test]
        public void Create_ClassWithoutElement_AddStarWithContains()
        {
            var xpath = GetXPathifyString(".beer");

            Assert.That(xpath, Is.EqualTo("//*[contains(@class,'beer')]"));
        }

        [Test]
        public void Create_ClassWithoutElementDescendsClassWithoutElement_AddStarWithContains()
        {
            var xpath = GetXPathifyString(".beer .soda");

            Assert.That(xpath, Is.EqualTo("//*[contains(@class,'beer')]/*[contains(@class,'soda')]"));
        }
    }
}