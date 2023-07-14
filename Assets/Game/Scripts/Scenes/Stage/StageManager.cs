using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;
	[Min(0.01f)] public float playerRespawnDelay = 1f;
	public Timer gameOverTextTimer;
	public StageUIManager uiManager;
	public EnemySpawnManager enemySpawnManager;
	public PlayerData playerData;

	public void InitiatePlayerRespawn(PlayerRobotRespawn prr) => StartCoroutine(RespawnPlayer(prr));

	public void SetGameAsOver()
	{
		gameOverTextTimer.StartTimer();
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

	private IEnumerator RespawnPlayer(PlayerRobotRespawn prr)
	{
		yield return new WaitForSeconds(playerRespawnDelay);

		prr.Respawn();
	}
}