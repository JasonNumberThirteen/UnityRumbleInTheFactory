using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
	public PlayerData playerData;
	public RectTransform parent; 
	public GameObject pointsText, defeatedEnemiesCounter, leftArrow, enemyType;
	public RectTransform totalText, horizontalLine;

	private void Start()
	{
		BuildPointsRows();
		SetTotalTextPosition();
	}

	private int DefeatedEnemiesTypes() => playerData.DefeatedEnemies.Count;

	private void BuildPointsRows()
	{
		int amount = 4;

		for (int i = 0; i < amount; ++i)
		{
			int y = -80 - 16*i;
			
			CreateElement(pointsText, 64, y);
			CreateElement(defeatedEnemiesCounter, 96, y);
			CreateElement(leftArrow, 112, y);
			CreateElement(enemyType, 0, y + 4);
		}
	}

	private void SetTotalTextPosition()
	{
		int offsetY = -16*4;
		
		totalText.anchoredPosition = new Vector2(totalText.anchoredPosition.x, totalText.anchoredPosition.y + offsetY);
		horizontalLine.anchoredPosition = new Vector2(horizontalLine.anchoredPosition.x, horizontalLine.anchoredPosition.y + offsetY);
	}

	private void CreateElement(GameObject element, float x, float y)
	{
		GameObject instance = Instantiate(element, parent);
		RectTransform rt = instance.GetComponent<RectTransform>();

		if(rt != null)
		{
			rt.anchoredPosition = new Vector2(x, y);
		}
	}
}