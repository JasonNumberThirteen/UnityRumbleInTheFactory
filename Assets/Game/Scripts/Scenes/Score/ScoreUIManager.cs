using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
	public PlayerData playerData;
	public RectTransform parent; 
	public GameObject pointsText, defeatedEnemiesCounter, leftArrow, enemyType;

	private void Start() => BuildPointsRows();
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