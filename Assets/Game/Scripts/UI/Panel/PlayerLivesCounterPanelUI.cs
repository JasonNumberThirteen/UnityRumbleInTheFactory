using UnityEngine;

public class PlayerLivesCounterPanelUI : MonoBehaviour
{
	[SerializeField] private PlayerData playerData;

	private IntCounter intCounter;

	public void UpdateCounterIfPossible()
	{
		if(playerData != null && intCounter != null)
		{
			intCounter.SetTo(playerData.Lives);
		}
	}

	private void Awake()
	{
		intCounter = GetComponentInChildren<IntCounter>();
	}

	private void Start()
	{
		UpdateCounterIfPossible();
	}
}