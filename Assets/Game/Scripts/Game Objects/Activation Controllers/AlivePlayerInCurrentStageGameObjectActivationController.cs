using UnityEngine;

[DefaultExecutionOrder(-150)]
public class AlivePlayerInCurrentStageGameObjectActivationController : MonoBehaviour
{
	[SerializeField] private PlayerRobotData playerRobotData;

	private void Awake()
	{
		if(playerRobotData != null)
		{
			gameObject.SetActive(playerRobotData.WasAliveOnCurrentStage);
		}
	}
}