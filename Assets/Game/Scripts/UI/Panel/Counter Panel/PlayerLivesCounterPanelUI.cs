using UnityEngine;

public class PlayerLivesCounterPanelUI : CounterPanelUI
{
	[SerializeField] private PlayerRobotData playerRobotData;

	private PlayerRobotsDataManager playerRobotsDataManager;

	protected override void Awake()
	{
		base.Awake();
		
		playerRobotsDataManager = FindAnyObjectByType<PlayerRobotsDataManager>(FindObjectsInactive.Include);

		RegisterToListeners(true);
	}

	private void Start()
	{
		UpdateCounterIfPossible();
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(playerRobotsDataManager != null)
			{
				playerRobotsDataManager.playerLivesChangedEvent.AddListener(UpdateCounterIfPossible);
			}
		}
		else
		{
			if(playerRobotsDataManager != null)
			{
				playerRobotsDataManager.playerLivesChangedEvent.RemoveListener(UpdateCounterIfPossible);
			}
		}
	}

	private void UpdateCounterIfPossible()
	{
		if(playerRobotData != null && intCounter != null)
		{
			intCounter.SetTo(playerRobotData.Lives);
		}
	}
}