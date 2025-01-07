using Random = UnityEngine.Random;
using UnityEngine;

public class BonusSpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject[] bonusesPrefabs;
	[SerializeField] private float bonusEnemyColorFadeTime = 0.5f;
	[SerializeField] private Color bonusEnemyColor;

	private StageSoundManager stageSoundManager;

	public float GetBonusEnemyColorFadeTime() => bonusEnemyColorFadeTime;
	public Color GetBonusEnemyColor() => bonusEnemyColor;

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
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
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