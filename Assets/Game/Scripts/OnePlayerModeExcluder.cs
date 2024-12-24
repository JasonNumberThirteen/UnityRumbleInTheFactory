using UnityEngine;

public class OnePlayerModeExcluder : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private void Awake()
	{
		var goShouldBeActive = gameData != null && gameData.twoPlayersMode;
		
		gameObject.SetActive(goShouldBeActive);
	}
}