using UnityEngine;

public class BonusSpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject[] bonusesPrefabs;

	private StageSoundManager stageSoundManager;

	public void SpawnRandomBonus()
	{
		var randomBonusGO = bonusesPrefabs.GetRandomElement();

		if(randomBonusGO == null)
		{
			return;
		}

		Instantiate(randomBonusGO);
		PlaySound();
	}

	private void Awake()
	{
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
	}

	private void PlaySound()
	{
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.BonusSpawn);
		}
	}
}