using UnityEngine;

public class PlayerLivesIntCounterPanelUI : IntCounterPanelUI
{
	[SerializeField] private PlayerRobotData playerRobotData;

	private PlayerRobotsDataManager playerRobotsDataManager;

	protected override void Awake()
	{
		base.Awake();
		
		playerRobotsDataManager = ObjectMethods.FindComponentOfType<PlayerRobotsDataManager>();

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
				playerRobotsDataManager.playerLivesWereChangedEvent.AddListener(OnPlayerLivesWereChanged);
			}
		}
		else
		{
			if(playerRobotsDataManager != null)
			{
				playerRobotsDataManager.playerLivesWereChangedEvent.RemoveListener(OnPlayerLivesWereChanged);
			}
		}
	}

	private void OnPlayerLivesWereChanged(int currentNumberOfLives, int differenceToCurrentNumberOfLives)
	{
		UpdateCounterIfPossible();
	}

	private void UpdateCounterIfPossible()
	{
		if(playerRobotData != null)
		{
			SetValueToCounter(playerRobotData.Lives);
		}
	}
}