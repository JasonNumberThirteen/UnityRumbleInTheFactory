using System;
using UnityEngine;

[Serializable]
public class StageSoundEffectsContainer
{
	[SerializeField] private AudioClip robotDamageSound;
	[SerializeField] private AudioClip playerRobotIdleSound;
	[SerializeField] private AudioClip playerRobotMovementSound;
	[SerializeField] private AudioClip playerRobotShootSound;
	[SerializeField] private AudioClip tileDestructionByPlayerRobotBulletSound;
	[SerializeField] private AudioClip playerRobotBulletHitSound;
	[SerializeField] private AudioClip playerRobotExplosionSound;
	[SerializeField] private AudioClip playerRobotLifeGainSound;
	[SerializeField] private AudioClip playerRobotSlidingSound;
	[SerializeField] private AudioClip enemyRobotExplosionSound;
	[SerializeField] private AudioClip nukeExplosionSound;
	[SerializeField] private AudioClip bonusSpawnSound;
	[SerializeField] private AudioClip bonusCollectSound;
	[SerializeField] private AudioClip gamePauseSound;

	public AudioClip GetAudioClipBySoundEffectType(SoundEffectType soundEffectType)
	{
		return soundEffectType switch
		{
			SoundEffectType.RobotDamage => robotDamageSound,
			SoundEffectType.PlayerRobotIdle => playerRobotIdleSound,
			SoundEffectType.PlayerRobotMovement => playerRobotMovementSound,
			SoundEffectType.PlayerRobotShoot => playerRobotShootSound,
			SoundEffectType.TileDestructionByPlayerRobotBullet => tileDestructionByPlayerRobotBulletSound,
			SoundEffectType.PlayerRobotBulletHit => playerRobotBulletHitSound,
			SoundEffectType.PlayerRobotExplosion => playerRobotExplosionSound,
			SoundEffectType.PlayerRobotLifeGain => playerRobotLifeGainSound,
			SoundEffectType.PlayerRobotSliding => playerRobotSlidingSound,
			SoundEffectType.EnemyRobotExplosion => enemyRobotExplosionSound,
			SoundEffectType.NukeExplosion => nukeExplosionSound,
			SoundEffectType.BonusSpawn => bonusSpawnSound,
			SoundEffectType.BonusCollect => bonusCollectSound,
			SoundEffectType.GamePause => gamePauseSound,
			_ => null
		};
	}
}