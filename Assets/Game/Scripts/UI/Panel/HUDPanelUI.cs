using UnityEngine;

[RequireComponent(typeof(RectTransformPositionController))]
public class HUDPanelUI : MonoBehaviour
{
	private StageFlowManager stageFlowManager;
	private RectTransformPositionController rectTransformPositionController;

	private void Awake()
	{
		stageFlowManager = FindAnyObjectByType<StageFlowManager>();
		rectTransformPositionController = GetComponent<RectTransformPositionController>();

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
			if(stageFlowManager != null)
			{
				stageFlowManager.stageActivatedEvent.AddListener(OnStageActivated);
			}
		}
		else
		{
			if(stageFlowManager != null)
			{
				stageFlowManager.stageActivatedEvent.RemoveListener(OnStageActivated);
			}
		}
	}

	private void OnStageActivated()
	{
		rectTransformPositionController.SetPositionX(0);
	}
}