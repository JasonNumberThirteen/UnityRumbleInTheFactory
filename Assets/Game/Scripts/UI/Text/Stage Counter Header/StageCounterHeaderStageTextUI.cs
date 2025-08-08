using UnityEngine;

public class StageCounterHeaderStageTextUI : MonoBehaviour
{
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;

	private void Awake()
	{
		translationBackgroundPanelUI = ObjectMethods.FindComponentOfType<TranslationBackgroundPanelUI>();
		
		RegisterToListeners(true);
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