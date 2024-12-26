using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI), typeof(LoopingCounter))]
public class StageSelectionStageCounterTextUI : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	
	private TextMeshProUGUI text;
	private LoopingCounter loopingCounter;

	public int GetCurrentCounterValue() => loopingCounter.CurrentValue;

	public void SetActive(bool active)
	{
		text.enabled = active;
	}

	public void ModifyCounterBy(int value)
	{
		if(value > 0)
		{
			loopingCounter.IncreaseBy(value);
		}
		else if(value < 0)
		{
			loopingCounter.DecreaseBy(Mathf.Abs(value));
		}
	}

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
		loopingCounter = GetComponent<LoopingCounter>();
	}

	private void Start()
	{
		if(gameData != null && gameData.stages != null && gameData.stages.Length > 0)
		{
			loopingCounter.SetRange(1, gameData.stages.Length);
		}
	}
}