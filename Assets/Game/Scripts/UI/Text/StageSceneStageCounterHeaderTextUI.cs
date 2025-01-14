using UnityEngine;

public class StageSceneStageCounterHeaderTextUI : StageCounterHeaderTextUI
{
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;

	protected override void Awake()
	{
		base.Awake();
		
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