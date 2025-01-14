using Random = UnityEngine.Random;
using UnityEngine;

public class BonusSpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject[] bonusesPrefabs;

	private StageSoundManager stageSoundManager;

	public void SpawnRandomBonus()
	{
		var randomBonusGO = GetRandomBonusGO();

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

	private GameObject GetRandomBonusGO()
	{
		var numberOfBonuses = bonusesPrefabs.Length;
		
		if(numberOfBonuses == 0)
		{
			return null;
		}
		
		var randomIndex = Random.Range(0, numberOfBonuses);

		return bonusesPrefabs[randomIndex];
	}
}