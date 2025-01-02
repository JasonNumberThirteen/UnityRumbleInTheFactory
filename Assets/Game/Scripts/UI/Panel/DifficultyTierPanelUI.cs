using UnityEngine;

public class DifficultyTierPanelUI : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private GameObject difficultyTierImageUIPrefab;

	private void Start()
	{
		if(difficultyTierImageUIPrefab == null)
		{
			return;
		}
		
		var difficultyTierIndex = gameData != null ? gameData.GetCurrentDifficultyTierIndex() : 0;
		
		for (var i = 0; i < difficultyTierIndex; ++i)
		{
			Instantiate(difficultyTierImageUIPrefab, gameObject.transform);
		}
	}
}