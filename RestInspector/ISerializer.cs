namespace RestInspector
{
	public interface ISerializer
	{
		string Serialize(object objectToSerialize);
	}
}