using System.Linq;
using HttpGhost.Navigation.Implementation;
using HttpGhost.Transport;
using Moq;
using NUnit.Framework;

namespace UnitTests.Navigation
{
    [TestFixture]
    public class NavigationResultTests
    {
        private NavigationResult navigationResult;
        private Mock<IRequest> requestMock;
        private Mock<IResponse> responseMock;

        [SetUp]
        public void Setup()
        {
            requestMock = new Mock<IRequest>();
            responseMock = new Mock<IResponse>();
            navigationResult = new NavigationResult(requestMock.Object, responseMock.Object);
        }

        [Test]
        public void Find_NotFindingAnything_ReturnEmptyList()
        {
            responseMock.Setup(r => r.Html).Returns("");

            var result = navigationResult.Find("//li");

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Find_FindingOneLiByXPath_ReturnListWithOneLi()
        {
            responseMock.Setup(r => r.Html).Returns("<li>bacon</li>");

            var result = navigationResult.Find("//li");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0), Is.EqualTo("<li>bacon</li>"));
        }

        [Test]
        public void Find_FindingLiByClassByXPath_ReturnListWithOneLi()
        {
            responseMock.Setup(r => r.Html).Returns("<li class=\"flurp\">bacon</li><li>arne</li>");

            var result = navigationResult.Find("//li[@class=\"flurp\"]");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ElementAt(0), Is.EqualTo("<li class=\"flurp\">bacon</li>"));
        }

        [Test]
        public void Find_FindingMultipleLiByXPath_ReturnListWithOneLi()
        {
            responseMock.Setup(r => r.Html).Returns("<li class=\"flurp\">bacon</li><li>arne</li>");

            var result = navigationResult.Find("//li");

            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}