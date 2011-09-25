namespace RestInspector.Navigation
{
	public interface INavigator
	{
		INavigationResult Get();
		INavigationResult Post(object postingObject);
	}
}