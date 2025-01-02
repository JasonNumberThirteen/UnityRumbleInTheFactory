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

	public void InstantiateRandomBonus()
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
		if(bonusesPrefabs.Length == 0)
		{
			return null;
		}
		
		var index = Random.Range(0, bonusesPrefabs.Length);

		return bonusesPrefabs[index];
	}
}