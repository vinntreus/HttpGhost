using System;

namespace WebTester.Navigation.Implementation
{
	public class Navigator : AbstractNavigator
	{
		public Navigator(Uri uri) : base(uri){}

		protected override void OnGet()
		{
			webRequest = CreateWebRequest();
		}
	}
}