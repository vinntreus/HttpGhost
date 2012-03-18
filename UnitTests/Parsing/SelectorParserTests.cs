using System.Linq;
using System.Collections.Generic;
using HttpGhost.Parsing;
using NUnit.Framework;

namespace UnitTests.Parsing
{
    [TestFixture]
    public class SelectorParserTests
    {
        private string ToXPath(string pattern)
        {
            return new SelectorParser(pattern).ToXPath();
        }

        [Test]
        public void Create_StartsWithTwoForwardSlash_DoNothing()
        {
            var xpath = ToXPath("//a");

            Assert.That(xpath, Is.EqualTo("//a"));
        }

        [Test]
        public void Create_StartsWithNoForwardSlash_AddTwoForwardSlash()
        {
            var xpath = ToXPath("a");

            Assert.That(xpath, Is.EqualTo("//a"));
        }

        [Test]
        public void Create_ContainDot_AddClassAttribute()
        {
            var xpath = ToXPath("a.beer");

            Assert.That(xpath, Is.EqualTo("//a[contains(@class,'beer')]"));
        }

        [Test]
        public void Create_ContainSharp_AddIdAttribute()
        {
            var xpath = ToXPath("a#beer");

            Assert.That(xpath, Is.EqualTo("//a[@id=\"beer\"]"));
        }

        [Test]
        public void Create_ContainDescendent_AddSlash()
        {
            var xpath = ToXPath("a#beer > li");

            Assert.That(xpath, Is.EqualTo("//a[@id=\"beer\"]/li"));
        }

        [Test]
        public void Create_ContainDescendentWithClass_AddSlash()
        {
            var xpath = ToXPath("a#beer > li.carlsberg");

            Assert.That(xpath, Is.EqualTo("//a[@id=\"beer\"]/li[contains(@class,'carlsberg')]"));
        }

        [Test]
        public void Create_IdWithoutElement_AddStar()
        {
            var xpath = ToXPath("#beer");

            Assert.That(xpath, Is.EqualTo("//*[@id=\"beer\"]"));
        }

        [Test]
        public void Create_ClassWithoutElement_AddStarWithContains()
        {
            var xpath = ToXPath(".beer");

            Assert.That(xpath, Is.EqualTo("//*[contains(@class,'beer')]"));
        }

        [Test]
        public void Create_ClassWithoutElementDescendsClassWithoutElement_AddStarWithContains()
        {
            var xpath = ToXPath(".beer .soda");

            Assert.That(xpath, Is.EqualTo("//*[contains(@class,'beer')]/*[contains(@class,'soda')]"));
        }
    }
}