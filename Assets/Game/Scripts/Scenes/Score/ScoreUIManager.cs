using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
	public PlayerData playerData;

	private void Start() => Debug.Log(DefeatedEnemiesTypes());
	private int DefeatedEnemiesTypes() => playerData.DefeatedEnemies.Count;
}