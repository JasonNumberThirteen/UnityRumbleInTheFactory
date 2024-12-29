using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextUI : MonoBehaviour
{
	protected TextMeshProUGUI text;

	protected virtual void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}
}