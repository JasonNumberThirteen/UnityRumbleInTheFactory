using UnityEngine;

public class EnemySpawner : EntitySpawner
{
	public bool IsBonus {get; set;}
	
	protected override GameObject EntityInstance()
	{
		GameObject instance = base.EntityInstance();

		AddBonusEnemyComponents(instance);

		return instance;
	}

	private void AddBonusEnemyComponents(GameObject instance)
	{
		if(IsBonus)
		{
			AddTriggerComponent(instance);
			AddColorComponent(instance);
			AddBonusComponent(instance);
		}
	}

	private void AddTriggerComponent(GameObject instance)
	{
		if(instance.TryGetComponent(out RobotTrigger rt))
		{
			Destroy(rt);
		}

		instance.AddComponent<BonusEnemyRobotTrigger>();
	}

	private void AddColorComponent(GameObject instance)
	{
		BonusEnemyRobotColor berc = instance.AddComponent<BonusEnemyRobotColor>();
		EnemySpawnManager esm = StageManager.instance.enemySpawnManager;

		berc.targetColor = esm.bonusEnemyTargetColor;
		berc.fadeTime = esm.bonusEnemyColorFadeTime;
	}

	private void AddBonusComponent(GameObject instance) => instance.AddComponent<BonusEnemyRobotBonus>();
}