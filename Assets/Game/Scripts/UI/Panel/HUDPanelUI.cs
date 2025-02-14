using UnityEngine;

[RequireComponent(typeof(RectTransformPositionController))]
public class HUDPanelUI : MonoBehaviour
{
	private RectTransformPositionController rectTransformPositionController;
	private StageSceneFlowManager stageSceneFlowManager;

	private void Awake()
	{
		rectTransformPositionController = GetComponent<RectTransformPositionController>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();

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