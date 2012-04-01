# HttpGhost

The main goal is to simplify integrationtesting on the .NET platform, foremost on websites using 
an authentication mechanism like basic authentication.

It is a lightweight framework with few external dependencies.

Fetch html/json from your website by a simple action. For html you can use css-selectors or xpath to search.
It is as easy to send data aswell.

E.g of some tests written in nUnit accessing a site with basic authentication:


<pre><code>[Test]
public void Get_SomeUrl_ReturnsSomeData()
{
	var session = new Session(USERNAME, PASSWORD);

	var result = session.Get(URL);

	Assert.That(result.ResponseContent, Is.StringContaining("somedata"));
}
	
[Test]
public void Get_URL_ReturnsTwoBeers()
{
	var session = new Session(USERNAME, PASSWORD);

	var elements = session.Get(URL).Find("li.beer");

	Assert.That(elements.Count(), Is.EqualTo(2));
}

[Test]
public void Post_Title_ReturnsTitle()
{
	var session = new Session(USERNAME, PASSWORD);

	var result = session.Post(new { Title = "jippi"}, url);

	Assert.That(result.ResponseContent, Is.StringContaining("jippi"));
}</code></pre>
