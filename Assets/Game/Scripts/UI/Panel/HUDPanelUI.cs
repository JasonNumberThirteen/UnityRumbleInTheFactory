using UnityEngine;

[RequireComponent(typeof(RectTransformPositionController))]
public class HUDPanelUI : MonoBehaviour
{
	private StageSceneFlowManager stageSceneFlowManager;
	private RectTransformPositionController rectTransformPositionController;

	private void Awake()
	{
		stageSceneFlowManager = FindAnyObjectByType<StageSceneFlowManager>();
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
			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageActivatedEvent.AddListener(OnStageActivated);
			}
		}
		else
		{
			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageActivatedEvent.RemoveListener(OnStageActivated);
			}
		}
	}

	private void OnStageActivated()
	{
		rectTransformPositionController.SetPositionX(0);
	}
}