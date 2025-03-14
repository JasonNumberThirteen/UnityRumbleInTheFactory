public class MainMenuGameSceneManager : GameSceneManager
{
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;

	private void Awake()
	{
		translationBackgroundPanelUI = ObjectMethods.FindComponentOfType<TranslationBackgroundPanelUI>();

		DeserialiseAllData();
		RegisterToListeners(true);
	}

	private void DeserialiseAllData()
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
				translationBackgroundPanelUI.panelFinishedTranslationEvent.AddListener(OnPanelFinishedTranslation);
			}
		}
		else
		{
			if(translationBackgroundPanelUI != null)
			{
				translationBackgroundPanelUI.panelFinishedTranslationEvent.RemoveListener(OnPanelFinishedTranslation);
			}
		}
	}
	
	private void OnPanelFinishedTranslation()
	{
		LoadSceneByName(STAGE_SELECTION_SCENE_NAME);
	}
}