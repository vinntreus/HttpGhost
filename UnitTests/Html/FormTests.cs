using System.Collections.Generic;
using HtmlAgilityPack;
using HttpGhost.Html;
using NUnit.Framework;

namespace UnitTests.Html
{
    [TestFixture]
    public class FormTests
    {
        private IDictionary<string, string> values;
        private Form form;

        [SetUp]
        public void Setup()
        {
            values = new Dictionary<string, string>();
        }

        [Test]
        public void InsertValue_EmptyForm_AddsValueWhenPosting()
        {
            var node = GetEmptyForm();
            SetupForm(node);
            form.InsertValue("a", "1");

            form.Submit();

            Assert.That(values["a"], Is.EqualTo("1"));
        }

        [Test]
        public void InsertValue_ExistingForm_ChangeAddExistingValue()
        {
            var node = GetFormWithOneInput();
            SetupForm(node);

            form.InsertValue("a", "2");

            form.Submit();

            Assert.That(values["a"], Is.EqualTo("2"));
        }

        [Test]
        public void Submit_ExistingForm_SubmitsForm()
        {
            var node = GetFormWithOneInput();
            SetupForm(node);

            form.Submit();

            Assert.That(values["a"], Is.EqualTo("1"));
        }

        [Test]
        public void GetFormData_MultipleName_UsesLastValue()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml("<form id='f' action='/'><input type='text' id='a' name='a' value='1' /><input type='text' id='a' name='a' value='1' /></form>");
            var node = htmlDoc.GetElementbyId("f");
            SetupForm(node);

            form.Submit();

            Assert.That(values["a"], Is.EqualTo("1"));
        }

        private static HtmlNode GetEmptyForm()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml("<form id='f' action='/'></form>");
            var node = htmlDoc.GetElementbyId("f");
            return node;
        }

        private static HtmlNode GetFormWithOneInput()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml("<form id='f' action='/'><input type='text' id='a' name='a' value='1' /></form>");
            var node = htmlDoc.GetElementbyId("f");
            return node;
        }

        private void SetupForm(HtmlNode node)
        {
            form = new Form(node)
            {
                OnSubmit = (o, s) =>
                {
                    values = (IDictionary<string, string>)o;
                    return null;
                }
            };
        }
    }
}