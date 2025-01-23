using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoreSoundManager : MonoBehaviour
{
	private AudioSource audioSource;
	private ScoreEnemyRobotTypeCountManager scoreEnemyRobotTypeCountManager;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		scoreEnemyRobotTypeCountManager = ObjectMethods.FindComponentOfType<ScoreEnemyRobotTypeCountManager>();

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
			if(scoreEnemyRobotTypeCountManager != null)
			{
				scoreEnemyRobotTypeCountManager.enemyRobotCountedEvent.AddListener(OnEnemyRobotCounted);
			}
		}
		else
		{
			if(scoreEnemyRobotTypeCountManager != null)
			{
				scoreEnemyRobotTypeCountManager.enemyRobotCountedEvent.RemoveListener(OnEnemyRobotCounted);
			}
		}
	}

	private void OnEnemyRobotCounted(List<PlayerRobotScoreData> playerRobotScoreDataList)
	{
		audioSource.Play();
	}
}