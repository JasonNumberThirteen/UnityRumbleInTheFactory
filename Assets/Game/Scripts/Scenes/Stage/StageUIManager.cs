using TMPro;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	public RectTransform parent;
	public GameObject gainedPointsCounter;

	public void CreateGainedPointsCounter(Vector2 position, int points)
	{
		GameObject instance = Instantiate(gainedPointsCounter, parent.transform);
		RectTransform rt = instance.GetComponent<RectTransform>();
		TextMeshProUGUI text = instance.GetComponent<TextMeshProUGUI>();
		Vector2 textPosition = new Vector2(position.x*16, position.y*16);

		rt.anchoredPosition = textPosition;
		text.text = points.ToString();
	}
}