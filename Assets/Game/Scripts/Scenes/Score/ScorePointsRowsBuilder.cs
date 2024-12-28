using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ScorePointsRowsBuilder : MonoBehaviour
{
	public PlayerData playerData;
	public GameData gameData;
	public RectTransform parent;
	public GameObject pointsText, defeatedEnemiesCounter, leftArrow, rightArrow, enemyType, enemyTypePointsCounter;

	public TextMeshProUGUI[] DefeatedEnemiesCounters {get; private set;}
	public TextMeshProUGUI[] EnemyTypePointsCounters {get; private set;}

	private Sprite[] defeatedEnemiesSprites;

	public void SetDefeatedEnemiesSprites(Sprite[] sprites) => defeatedEnemiesSprites = sprites;

	public void BuildPointsRows()
	{
		int amount = DefeatedEnemiesTypes();

		DefeatedEnemiesCounters = new TextMeshProUGUI[amount];
		EnemyTypePointsCounters = new TextMeshProUGUI[amount];

		for (int i = 0; i < amount; ++i)
		{
			int y = -80 - 16*i;
			
			InstantiateElement(pointsText, new Vector2(64, y));
			InstantiateElement(defeatedEnemiesCounter, new Vector2(96, y), i, OnDefeatedEnemiesCounterInstantiate);
			InstantiateElement(leftArrow, new Vector2(112, y));
			InstantiateElement(enemyType, new Vector2(0, y + 4), i, OnEnemyTypeSpriteInstantiate);
			InstantiateElement(enemyTypePointsCounter, new Vector2(16, y), i, OnEnemyTypePointsCounterInstantiate);

			if(gameData.SelectedTwoPlayersMode)
			{
				InstantiateElement(pointsText, new Vector2(208, y));
				InstantiateElement(defeatedEnemiesCounter, new Vector2(144, y), i, OnDefeatedEnemiesCounterInstantiate);
				InstantiateElement(rightArrow, new Vector2(136, y));
				InstantiateElement(enemyTypePointsCounter, new Vector2(160, y), i, OnEnemyTypePointsCounterInstantiate);
			}
		}
	}

	private int DefeatedEnemiesTypes() => playerData.DefeatedEnemies.Count;

	private GameObject InstantiateElement(GameObject element, Vector2 position, int index, Action<GameObject, int> onInstantiate)
	{
		GameObject instance = InstantiateElement(element, position);

		onInstantiate(instance, index);

		return instance;
	}

	private GameObject InstantiateElement(GameObject element, Vector2 position)
	{
		GameObject instance = Instantiate(element, parent);
		
		if(instance.TryGetComponent(out RectTransform rt))
		{
			rt.anchoredPosition = position;
		}

		return instance;
	}

	private void OnDefeatedEnemiesCounterInstantiate(GameObject instance, int index)
	{
		if(instance.TryGetComponent(out TextMeshProUGUI text))
		{
			DefeatedEnemiesCounters[index] = text;
		}
	}

	private void OnEnemyTypeSpriteInstantiate(GameObject instance, int index)
	{
		if(instance.TryGetComponent(out Image image))
		{
			image.sprite = defeatedEnemiesSprites[index];
		}
	}

	private void OnEnemyTypePointsCounterInstantiate(GameObject instance, int index)
	{
		if(instance.TryGetComponent(out TextMeshProUGUI text))
		{
			EnemyTypePointsCounters[index] = text;
		}
	}
}