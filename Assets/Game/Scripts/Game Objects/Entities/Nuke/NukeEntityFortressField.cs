using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Timer))]
public class NukeEntityFortressField : MonoBehaviour
{
	[SerializeField] private LayerMask overlapLayerMask;
	[SerializeField] private GameObject tilePrefab;
	[SerializeField, Min(0.01f)] private float timeForBlinkStart = 5f;
	[SerializeField, Min(0.01f)] private float blinkDuration = 0.25f;
	[SerializeField] private Sprite tileSpriteToBlink;
	[SerializeField] private GameObject tileToSpawnAfterElapsedTimePrefab;
	[SerializeField, Min(0.01f)] private float gridSize = 0.5f;

	private NukeEntity nukeEntity;
	private Collider2D c2D;
	private Timer timer;
	private readonly List<GameObject> fortressTileGOs = new();

	public void SpawnFortress(float duration)
	{
		fortressTileGOs.Clear();
		DestroyAllGOsWithinArea();
		SpawnTilesWithinArea();
		timer.SetDuration(duration);
		timer.StartTimer();
	}

	public void DestroyAllGOsWithinArea()
	{
		var colliders = Physics2D.OverlapBoxAll(c2D.bounds.center, c2D.bounds.size, 0f, overlapLayerMask);

		foreach (var collider in colliders)
		{
			Destroy(collider.gameObject);
		}
	}

	private void Awake()
	{
		nukeEntity = GetComponentInParent<NukeEntity>();
		c2D = GetComponent<Collider2D>();
		timer = GetComponent<Timer>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.timerFinishedEvent.AddListener(OnTimerFinished);
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
		}
	}

	private void OnTimerFinished()
	{
		if(tileToSpawnAfterElapsedTimePrefab == null)
		{
			return;
		}

		fortressTileGOs.ForEach(go =>
		{
			Instantiate(tileToSpawnAfterElapsedTimePrefab, go.transform.position, go.transform.rotation);
			Destroy(go);
		});

		fortressTileGOs.Clear();
	}

	private void SpawnTilesWithinArea()
	{
		for (var y = c2D.bounds.min.y; y < c2D.bounds.max.y; y += gridSize)
		{
			for (var x = c2D.bounds.min.x; x < c2D.bounds.max.x; x += gridSize)
			{
				SpawnTile(GetTilePosition(x, y));
			}
		}
	}

	private Vector2 GetTilePosition(float leftSideX, float topSideY)
	{
		var halfOfGridSize = gridSize*0.5f;
		var x = leftSideX + halfOfGridSize;
		var y = topSideY + halfOfGridSize;
		
		return new Vector2(x, y);
	}

	private void SpawnTile(Vector2 position)
	{
		if(tilePrefab == null || (nukeEntity != null && nukeEntity.OverlapPoint(position)))
		{
			return;
		}

		var instance = Instantiate(tilePrefab, position, Quaternion.identity);
		
		if(instance != null)
		{
			var nukeEntityFortressFieldTileRenderer = instance.AddComponent<NukeEntityFortressFieldTileRenderer>();

			nukeEntityFortressFieldTileRenderer.Setup(timer.GetDuration(), timeForBlinkStart, blinkDuration, tileSpriteToBlink);
			fortressTileGOs.Add(instance);
		}
	}
}