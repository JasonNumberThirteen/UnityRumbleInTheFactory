using UnityEngine;

[RequireComponent(typeof(IntCounter))]
public class StageCounterHeaderTextUI : TextUI
{
	[SerializeField] private GameData gameData;

	private IntCounter intCounter;

	protected override void Awake()
	{
		intCounter = GetComponent<IntCounter>();
	}

	private void Start()
	{
		if(gameData != null)
		{
			intCounter.SetTo(gameData.StageNumber);
		}
	}
}