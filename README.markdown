# HttpGhost

## Why
The main goal is to simplify integrationtesting on the .NET platform, foremost on websites using 
an authentication mechanism like basic authentication. This includes to run them fast, without the hassle
of having to start up a real browser or to install browser version xyz on your machine.

## What
You could think of HttpGhost as a simple web browser (currently without javascript support) with capabilites
to easily find html elements by css-selectors (or xpath), to follow links or post forms. But it does not stop with html,
HttpGhost can also handle JSON and has built in support (by using JSON.NET) to easily convert a JSON-response to 
your desired type.

## How

It can be installed using nuget: <code>Install-Package HttpGhost</code>

HttpGhost can be used with any unit test framework, which makes it easy to plugin with your existing CI-server. 
It could be used in a simple console application to try out some JSON api or what not.

### Dependencies
* JSON.NET
* HtmlAgilityPack

### Contribute
If you want to make a contribution, that would be great! I would suggest you to first create a issue regarding the
change you want to make, fork and create a pull request.

### Examples

#### HTTP (basically only request, response using anonymous authentication)

<pre><code>[Test]
public void Get_SomeUrl_ReturnsSomeData()
{
	var session = new HttpSession();

	var result = session.Get(URL);

	Assert.That(result.Response.Body, Is.StringContaining("somedata"));
}

[Test]
public void Get_SomeUrl_IsPdf()
{
	var session = new HttpSession();

	var result = session.Get(URL);

	Assert.That(result.Response.Headers[HttpResponseHeader.ContentType], Is.StringContaining("pdf"));
}

[Test]
public void Post_SomeUrl_IsPdf()
{
	var session = new HttpSession();

	var result = session.Post(URL);

	Assert.That(result.Response.Headers[HttpResponseHeader.ContentType], Is.StringContaining("pdf"));
}

[Test]
public void Post_URL_ReturnCreated()
{
	var session = new HttpSession();
	
	var result = session.Post(URL, new { title = "jippi" });
	
	Assert.That(result.Response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
}

[Test]
public void Put_URL_ReturnHtml()
{
	var session = new HttpSession();
	
	var result = session.Put(URL, new { Title = "jippi" });

	Assert.That(result.Response.Body, Is.StringContaining("Putting"));
}

[Test]
public void Delete_URL_ReturnHtml()
{
	var session = new HttpSession();
	
	var result = session.Delete(URL, new { id = 2 });

	Assert.That(result.Response.Body, Is.StringContaining("Deleting"));
}</code></pre>

#### Html

<pre><code>[Test]
public void Get_URL_ReturnsTwoListItems()
{
	var session = new HtmlSession();

	var elements = session.Get(URL).Find("li.beer"); //GET-request on URL, then selects out all li-elements which has the cssclass beer

	Assert.That(elements.Count(), Is.EqualTo(2));
}

[Test]
public void Get_URL_LinkWorks()
{
	var session = new HtmlSession();
	
	var result = session.Get(URL).Follow("#mylink"); //first GET-request on URL, then another GET-request using the href of a#mylink
	
	Assert.That(result.Response.Body, Is.EqualTo("Followed"));
}

[Test]
public void OnURL_SubmitForm_ReturnsResult()
{	
	var session = new HtmlSession();
	var form = session.Get(URL).FindForm("#form"); //GET-request and find form with Id="form"	
	form.SetValue("#input1", "monkey"); //find input with id="input1" and set its value="monkey"
	
	var result = form.Submit(); //submit form (POST the form values using the forms action attribute)
	
	Assert.That(result.Response.Body, Is.StringContaining("monkey"));
}</code></pre>

#### Json
<pre><code>[Test]
public void Get_URL_CanConvertResultToObject()
{
	var session = new JsonSession();
	var result = session.Get(URL);

	var obj = result.To<TestClass>(); //converts to any class, or dynamic

	Assert.That(obj.A, Is.EqualTo("b"));
}

</code></pre>

#### Basic authentication, (For any type of session, just pass username and password in ctor)
	
<pre><code>[Test]
public void Get_URL_ReturnsTwoBeers()
{
	var session = new HttpSession(USERNAME, PASSWORD);

	...
}
</code></pre>
