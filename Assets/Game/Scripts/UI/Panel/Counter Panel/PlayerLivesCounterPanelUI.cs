using UnityEngine;

public class PlayerLivesCounterPanelUI : CounterPanelUI
{
	[SerializeField] private PlayerData playerData;

	private PlayersDataManager playersDataManager;

	protected override void Awake()
	{
		base.Awake();
		
		playersDataManager = FindAnyObjectByType<PlayersDataManager>(FindObjectsInactive.Include);

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
			if(playersDataManager != null)
			{
				playersDataManager.playerLivesChangedEvent.AddListener(UpdateCounterIfPossible);
			}
		}
		else
		{
			if(playersDataManager != null)
			{
				playersDataManager.playerLivesChangedEvent.RemoveListener(UpdateCounterIfPossible);
			}
		}
	}

	private void UpdateCounterIfPossible()
	{
		if(playerData != null && intCounter != null)
		{
			intCounter.SetTo(playerData.Lives);
		}
	}
}