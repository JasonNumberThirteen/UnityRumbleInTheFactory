using UnityEngine;

[DefaultExecutionOrder(-200)]
public class TwoPlayersModeGameObjectActivationController : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private void Awake()
	{
		if(gameData != null)
		{
			gameObject.SetActive(gameData.SelectedTwoPlayersMode);
		}
	}
}