using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StageSelectionNoStagesTextUI : MonoBehaviour
{
	private TextMeshProUGUI text;

	public void SetActive(bool active)
	{
		text.enabled = active;
	}

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}
}