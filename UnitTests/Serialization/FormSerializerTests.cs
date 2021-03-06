using System.Linq;
using System.Collections.Generic;
using HttpGhost.Serialization;
using NUnit.Framework;

namespace UnitTests.Serialization
{
	[TestFixture]
	public class FormSerializerTests
	{
		private FormSerializer formserializer;

		[SetUp]
		public void Setup()
		{
			formserializer = new FormSerializer();
		}

		[Test]
		public void Serialize_ObjectWithOneStringProperty_ReturnPropertynameFirst()
		{
			var result = formserializer.Serialize(new {Title = "a"});

			Assert.That(result, Is.StringStarting("Title"));
		}

		[Test]
		public void Serialize_ObjectWithOneStringProperty_ReturnEqualSignAfterPropertyname()
		{
			var result = formserializer.Serialize(new { Title = "a" });

			Assert.That(result, Is.StringStarting("Title="));
		}

		[Test]
		public void Serialize_ObjectWithOneStringProperty_ReturnValueAfterEqualsign()
		{
			var result = formserializer.Serialize(new { Title = "a" });

			Assert.That(result, Is.StringEnding("=a"));
		}

		[Test]
		public void Serialize_String_ReturnString()
		{
			var result = formserializer.Serialize("aj");

			Assert.That(result, Is.EqualTo("aj"));
		}

		[Test]
		public void Serialize_WithMultipleProperties_ShouldSeparateWithAmpersand()
		{
			var result = formserializer.Serialize(new { Title = "a", Fuddle = "b" });

			Assert.That(result, Is.StringEnding("Title=a&Fuddle=b"));
		}

	    [Test]
	    public void Serialize_DictionaryOfStrings_UsesKeyAndValue()
	    {
	        IDictionary<string, string> data = new Dictionary<string, string> { {"key", "value"}};

	        var result = formserializer.Serialize(data);

	        Assert.That(result, Is.StringContaining("key=value"));
	    }

        [Test]
        public void Serialize_DictionaryOfStrings_EachKeyValueIsSeparatedWithAmpersant()
        {
            IDictionary<string, string> data = new Dictionary<string, string> { { "key", "value" }, {"key2", "value2"} };

            var result = formserializer.Serialize(data);

            Assert.That(result, Is.EqualTo("key=value&key2=value2"));
        }
	}
}