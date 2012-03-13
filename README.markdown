# HttpGhost

The main goal is to simplify integrationtesting on the .NET platform, foremost on websites using 
an authentication mechanism like basic authentication.

It is a lightweight framework with no external dependencies.


E.g of a test written in nUnit accessing a site with basic authentication:

[Test]
public void SomeUrl_Get_ReturnSomeData()
{
	var session = new Session(USERNAME, PASSWORD);

	var result = session.Get(URL);

	Assert.That(result.ResponseContent, Is.StringContaining("somedata"));
}