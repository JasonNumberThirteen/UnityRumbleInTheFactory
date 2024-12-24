using UnityEngine;

public class OnePlayerModeExcluder : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private void Awake() => gameObject.SetActive(gameData.twoPlayersMode);
}