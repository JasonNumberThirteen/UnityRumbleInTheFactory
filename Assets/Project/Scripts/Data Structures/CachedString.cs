using System.Text;

public class CachedString
{
	private readonly StringBuilder stringBuilder = new();

	public string GetCachedString() => stringBuilder.ToString();

	public void SetString(string newString)
	{
		if(newString.Equals(stringBuilder.ToString()))
		{
			return;
		}
		
		stringBuilder.Clear();
		stringBuilder.Append(newString);
	}
}