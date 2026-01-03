public class MainMenuGameSceneManager : GameSceneManager
{
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;

	private void Awake()
	{
		translationBackgroundPanelUI = ObjectMethods.FindComponentOfType<TranslationBackgroundPanelUI>();

		DeserialiseAllDataIfPossible();
		RegisterToListeners(true);
	}

	private void DeserialiseAllDataIfPossible()
	{
		var dataSerialisationManager = ObjectMethods.FindComponentOfType<DataSerialisationManager>();

		if(dataSerialisationManager != null)
		{
			dataSerialisationManager.DeserialiseAllDataIfPossible();
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
				translationBackgroundPanelUI.translationWasFinishedEvent.AddListener(OnTranslationWasFinished);
			}
		}
		else
		{
			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.translationWasFinishedEvent.RemoveListener(OnTranslationWasFinished);
			}
		}
	}
	
	private void OnTranslationWasFinished()
	{
		LoadSceneByName(STAGE_SELECTION_SCENE_NAME);
	}
}