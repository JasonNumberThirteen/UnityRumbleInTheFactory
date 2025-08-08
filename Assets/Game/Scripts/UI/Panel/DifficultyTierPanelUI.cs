using UnityEngine;

public class DifficultyTierPanelUI : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private GameObject difficultyTierImageUIPrefab;

	private TranslationBackgroundPanelUI translationBackgroundPanelUI;

	private void Awake()
	{
		translationBackgroundPanelUI = ObjectMethods.FindComponentOfType<TranslationBackgroundPanelUI>();

		RegisterToListeners(true);
	}

	private void Start()
	{
		if(difficultyTierImageUIPrefab == null)
		{
			return;
		}
		
		var difficultyTierIndex = GameDataMethods.GetCurrentDifficultyTierIndex(gameData);
		
		for (var i = 0; i < difficultyTierIndex; ++i)
		{
			Instantiate(difficultyTierImageUIPrefab, gameObject.transform);
		}
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.translationWasStartedEvent.AddListener(OnPanelStartedTranslation);
			}
		}
		else
		{
			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.translationWasStartedEvent.RemoveListener(OnPanelStartedTranslation);
			}
		}
	}

	private void OnPanelStartedTranslation()
	{
		gameObject.SetActive(false);
	}
}