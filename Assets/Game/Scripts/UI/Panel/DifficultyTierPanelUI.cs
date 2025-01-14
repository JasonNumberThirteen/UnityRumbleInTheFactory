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
		
		var difficultyTierIndex = gameData != null ? gameData.GetCurrentDifficultyTierIndex() : 0;
		
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
				translationBackgroundPanelUI.panelStartedTranslationEvent.AddListener(OnPanelStartedTranslation);
			}
		}
		else
		{
			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.panelStartedTranslationEvent.RemoveListener(OnPanelStartedTranslation);
			}
		}
	}

	private void OnPanelStartedTranslation()
	{
		gameObject.SetActive(false);
	}
}