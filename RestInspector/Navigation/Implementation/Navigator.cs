using System;

namespace RestInspector.Navigation.Implementation
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