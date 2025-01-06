using UnityEngine;

public class StageCounterPanelUI : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private IntCounter intCounter;

	private void Awake()
	{
		intCounter = GetComponentInChildren<IntCounter>();
	}

	private void Start()
	{
		if(gameData != null && intCounter != null)
		{
			intCounter.SetTo(gameData.StageNumber);
		}
	}
}