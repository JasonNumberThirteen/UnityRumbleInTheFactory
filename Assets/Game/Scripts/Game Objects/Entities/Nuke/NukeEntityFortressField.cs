using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Timer))]
public class NukeEntityFortressField : MonoBehaviour
{
	[SerializeField] private LayerMask destroyableGameObjectsWithinArea;
	[SerializeField] private GameObject tilePrefab;
	[SerializeField, Min(0.01f)] private float timeForBlinkStart = 5f;
	[SerializeField, Min(0.01f)] private float blinkDuration = 0.25f;
	[SerializeField] private Sprite tileSpriteToSwitch;
	[SerializeField] private GameObject tileToSpawnAfterElapsedTimePrefab;

	private Collider2D c2D;
	private Timer timer;
	private NukeEntity nukeEntity;
	private StageLayoutManager stageLayoutManager;

	private readonly List<GameObject> fortressTileGOs = new();
	private readonly float DESTRUCTION_AREA_SIZE_OFFSET = 0.1f;

	public void SpawnFortress(float duration)
	{
		timer.StartTimerWithSetDuration(duration);
		fortressTileGOs.Clear();
		DestroyAllGOsWithinArea();
		SpawnTilesWithinArea();
	}

	public void DestroyAllGOsWithinArea()
	{
		var size = (Vector2)c2D.bounds.size - Vector2.one*DESTRUCTION_AREA_SIZE_OFFSET;
		var colliders = Physics2D.OverlapBoxAll(c2D.bounds.center, size, 0f, destroyableGameObjectsWithinArea);

		colliders.ForEach(collider => Destroy(collider.gameObject));
	}

	private void Awake()
	{
		c2D = GetComponent<Collider2D>();
		timer = GetComponent<Timer>();
		nukeEntity = GetComponentInParent<NukeEntity>();
		stageLayoutManager = ObjectMethods.FindComponentOfType<StageLayoutManager>();

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
		fortressTileGOs.ForEach(ReplaceTileAfterElapsedTime);
		fortressTileGOs.Clear();
	}

	private void ReplaceTileAfterElapsedTime(GameObject go)
	{
		if(go == null)
		{
			return;
		}
		
		if(tileToSpawnAfterElapsedTimePrefab != null)
		{
			Instantiate(tileToSpawnAfterElapsedTimePrefab, go.transform.position, go.transform.rotation);
		}
		
		Destroy(go);
	}

	private void SpawnTilesWithinArea()
	{
		if(stageLayoutManager == null)
		{
			return;
		}

		var tileSize = stageLayoutManager.GetTileSize();
		
		for (var y = c2D.bounds.min.y; y < c2D.bounds.max.y; y += tileSize)
		{
			for (var x = c2D.bounds.min.x; x < c2D.bounds.max.x; x += tileSize)
			{
				SpawnTile(GetTilePosition(x, y, tileSize));
			}
		}
	}

	private Vector2 GetTilePosition(float leftSideX, float topSideY, float tileSize)
	{
		var halfOfTileSize = tileSize*0.5f;
		var x = leftSideX + halfOfTileSize;
		var y = topSideY + halfOfTileSize;
		
		return new Vector2(x, y);
	}

	private void SpawnTile(Vector2 position)
	{
		if(tilePrefab == null || (nukeEntity != null && nukeEntity.OverlapPoint(position)))
		{
			return;
		}

		var instance = Instantiate(tilePrefab, position, Quaternion.identity);
		var nukeEntityFortressFieldTileRenderer = instance.AddComponent<NukeEntityFortressFieldTileRenderer>();

		nukeEntityFortressFieldTileRenderer.Setup(timer.GetDuration(), timeForBlinkStart, blinkDuration, tileSpriteToSwitch);
		fortressTileGOs.Add(instance);
	}
}