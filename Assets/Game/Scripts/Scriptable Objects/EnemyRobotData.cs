using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Robot Data")]
public class EnemyRobotData : RobotData
{
	public override int RankNumber
	{
		get => rankNumber;
		set => rankNumber = Mathf.Clamp(value, 1, ranks != null && ranks.Length > 0 ? ranks.Length : 1);
	}
	
	[SerializeField] private GameObject prefab;
	[SerializeField, Min(0)] private int pointsForDefeat;
	[SerializeField] private Sprite displayInScoreSceneSprite;
	[SerializeField] private RobotRank[] ranks;

	private int rankNumber;

	public override RobotRank GetRank() => ranks[RankNumber - 1];
	public GameObject GetPrefab() => prefab;
	public int GetPointsForDefeat() => pointsForDefeat;
	public Sprite GetDisplayInScoreSceneSprite() => displayInScoreSceneSprite;
}