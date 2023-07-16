using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;
	[Min(0.01f)] public float playerRespawnDelay = 1f;
	public StageUIManager uiManager;
	public EnemySpawnManager enemySpawnManager;
	public PlayerData playerData;
	public Timer gameOverTimer;

	public void InitiatePlayerRespawn(PlayerRobotRespawn prr) => prr.Respawn();

	public void SetGameAsOver()
	{
		gameOverTimer.StartTimer();
	}
	
	private void Awake() => CheckSingleton();
	private void Start() => playerData.ResetData();

	private void CheckSingleton()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}
}