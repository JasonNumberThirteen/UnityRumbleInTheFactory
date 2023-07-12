using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	private void Awake() => CheckSingleton();

	private void CheckSingleton()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}
}