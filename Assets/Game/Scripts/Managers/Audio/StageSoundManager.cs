using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageSoundManager : MonoBehaviour
{
	public UnityEvent<SoundEffectType> soundPlayedEvent;
	
	[SerializeField, Range(0, 8)] private int additionalSoundChannels = 4;
	[SerializeField] private AudioClip robotDamageSound;
	[SerializeField] private AudioClip playerRobotIdleSound;
	[SerializeField] private AudioClip playerRobotMovementSound;
	[SerializeField] private AudioClip playerRobotShootSound;
	[SerializeField] private AudioClip playerRobotDestructibleTileDestructionSound;
	[SerializeField] private AudioClip playerRobotBulletHitSound;
	[SerializeField] private AudioClip playerRobotExplosionSound;
	[SerializeField] private AudioClip playerRobotLifeGainSound;
	[SerializeField] private AudioClip playerRobotSlidingSound;
	[SerializeField] private AudioClip enemyRobotExplosionSound;
	[SerializeField] private AudioClip nukeExplosionSound;
	[SerializeField] private AudioClip bonusSpawnSound;
	[SerializeField] private AudioClip bonusCollectSound;
	[SerializeField] private AudioClip gamePauseSound;

	private readonly List<SoundChannel> soundChannels = new();
	private PlayerRobotMovementSoundChannel playerRobotMovementSourceChannel;
	private StageMusicManager stageMusicManager;
	private StageStateManager stageStateManager;
	private bool canPlaySounds;

	private readonly string CHANNEL_GO_NAME = "Channel";

	public void PlaySound(SoundEffectType soundEffectType)
	{
		if(!canPlaySounds)
		{
			return;
		}
		
		var soundChannel = GetSoundChannelBySoundEffectType(soundEffectType);

		if(soundChannel == null)
		{
			return;
		}

		soundChannel.Play(GetAudioClipBySoundEffectTypeIfPossible(soundEffectType));
		soundPlayedEvent?.Invoke(soundEffectType);
	}

	public AudioClip GetAudioClipBySoundEffectType(SoundEffectType soundEffectType)
	{
		return soundEffectType switch
		{
			SoundEffectType.RobotDamage => robotDamageSound,
			SoundEffectType.PlayerRobotIdle => playerRobotIdleSound,
			SoundEffectType.PlayerRobotMovement => playerRobotMovementSound,
			SoundEffectType.PlayerRobotShoot => playerRobotShootSound,
			SoundEffectType.PlayerRobotDestructibleTileDestruction => playerRobotDestructibleTileDestructionSound,
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
	
	private void Awake()
	{
		playerRobotMovementSourceChannel = GetComponentInChildren<PlayerRobotMovementSoundChannel>();
		stageMusicManager = ObjectMethods.FindComponentOfType<StageMusicManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();

		RegisterToListeners(true);
	}

	private void Start()
	{
		for (var i = 1; i <= additionalSoundChannels; ++i)
		{
			var childChannelGO = new GameObject($"{CHANNEL_GO_NAME} {i}");
			var soundChannel = childChannelGO.AddComponent<SoundChannel>();

			childChannelGO.transform.SetParent(gameObject.transform);
			soundChannels.Add(soundChannel);
		}
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(stageMusicManager != null)
			{
				stageMusicManager.musicStoppedPlayingEvent.AddListener(OnMusicStoppedPlaying);
			}
		}
		else
		{
			if(stageMusicManager != null)
			{
				stageMusicManager.musicStoppedPlayingEvent.RemoveListener(OnMusicStoppedPlaying);
			}
		}
	}

	private void OnMusicStoppedPlaying()
	{
		if(stageStateManager == null || !stageStateManager.StateIsSetTo(StageState.Over))
		{
			canPlaySounds = true;
		}
	}

	private AudioClip GetAudioClipBySoundEffectTypeIfPossible(SoundEffectType soundEffectType)
	{
		var conditionsBySoundEffectType = new Dictionary<SoundEffectType, bool>()
		{
			{SoundEffectType.PlayerRobotIdle, stageStateManager == null || !stageStateManager.StateIsSetTo(StageState.Won)}
		};

		return !conditionsBySoundEffectType.ContainsKey(soundEffectType) || conditionsBySoundEffectType[soundEffectType] ? GetAudioClipBySoundEffectType(soundEffectType) : null;
	}

	private SoundChannel GetSoundChannelBySoundEffectType(SoundEffectType soundEffectType)
	{
		var playerRobotTypes = new List<SoundEffectType>
		{
			SoundEffectType.PlayerRobotIdle,
			SoundEffectType.PlayerRobotMovement
		};

		return playerRobotTypes.Contains(soundEffectType) ? playerRobotMovementSourceChannel : soundChannels.FirstOrDefault(soundChannel => !soundChannel.SoundIsPlaying());
	}
}