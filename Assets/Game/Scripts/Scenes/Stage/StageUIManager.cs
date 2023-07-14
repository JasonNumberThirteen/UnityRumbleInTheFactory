using TMPro;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	public RectTransform parent;
	public GameObject gainedPointsCounter;
	public PlayerData playerData;
	public GameData gameData;
	public Counter playerOneLivesCounter, stageCounter;

	public void CreateGainedPointsCounter(Vector2 position, int points)
	{
		GameObject instance = Instantiate(gainedPointsCounter, parent.transform);
		RectTransform rt = instance.GetComponent<RectTransform>();
		TextMeshProUGUI text = instance.GetComponent<TextMeshProUGUI>();
		Vector2 textPosition = new Vector2(position.x*16, position.y*16);

		rt.anchoredPosition = textPosition;
		text.text = points.ToString();
	}

	public void UpdateCounters()
	{
		playerOneLivesCounter.SetTo(playerData.lives);
		stageCounter.SetTo(gameData.stage);
	}

	private void Start() => UpdateCounters();
}