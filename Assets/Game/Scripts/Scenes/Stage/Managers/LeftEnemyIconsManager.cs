using UnityEngine;

public class LeftEnemyIconsManager : MonoBehaviour
{
	public RectTransform hud;
	public GameObject leftEnemyIcon;
	[Min(0)] public int leftEnemyIconsLimit = 20;
	
	private GameObject[] leftEnemyIcons;
	private int leftEnemyIconIndex;

	public void DestroyLeftEnemyIcon()
	{
		if(--leftEnemyIconIndex < leftEnemyIcons.Length)
		{
			Destroy(leftEnemyIcons[leftEnemyIconIndex]);
		}
	}
	
	private void Start() => InstantiateIcons();

	private int LeftEnemyIconsCount(int enemiesCount) => Mathf.Min(enemiesCount, leftEnemyIconsLimit);
	private int LeftEnemyIconX(int index) => 8*(index % 2);
	private int LeftEnemyIconY(int index) => -8*(index >> 1);
	private Vector2 LeftEnemyIconPosition(int index)
	{
		int offsetX = LeftEnemyIconX(index);
		int offsetY = LeftEnemyIconY(index);
		Vector2 initialPosition = new Vector2(-16, -16);
		Vector2 offset = new Vector2(offsetX, offsetY);
		
		return initialPosition + offset;
	}

	private void InstantiateIcons()
	{
		int enemiesCount = StageManager.instance.enemySpawnManager.EnemiesCount();
		int iconsCount = LeftEnemyIconsCount(enemiesCount);

		leftEnemyIcons = new GameObject[iconsCount];
		leftEnemyIconIndex = enemiesCount;

		for (int i = 0; i < iconsCount; ++i)
		{
			InstantiateLeftEnemyIcon(i);
		}
	}

	private void InstantiateLeftEnemyIcon(int index)
	{
		GameObject instance = Instantiate(leftEnemyIcon, hud.transform);
		
		if(instance.TryGetComponent(out RectTransformPositionController rtm))
		{
			rtm.SetPosition(LeftEnemyIconPosition(index));
		}

		leftEnemyIcons[index] = instance;
	}
}