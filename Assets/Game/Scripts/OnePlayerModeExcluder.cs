using UnityEngine;

public class OnePlayerModeExcluder : MonoBehaviour
{
	public GameData gameData;

	private void Awake() => gameObject.SetActive(gameData.twoPlayersMode);
}