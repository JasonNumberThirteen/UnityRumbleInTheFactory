using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Robot Data")]
public class EnemyRobotData : RobotData
{
	[SerializeField, Min(1)] private int ordinalNumber;
	[SerializeField] private GameObject prefab;
	[SerializeField, Min(0)] private int pointsForDefeat;
	[SerializeField] private Sprite displayInScoreSceneSprite;
	[SerializeField] private EnemyRobotRank[] ranks;

	public override RobotRank GetRankByIndex(int index) => index >= 0 && index < GetNumberOfRanks() ? ranks[index] : null;
	public override int GetNumberOfRanks() => ranks != null && ranks.Length > 0 ? ranks.Length : 1;

	public int GetOrdinalNumber() => ordinalNumber;
	public GameObject GetPrefab() => prefab;
	public int GetPointsForDefeat() => pointsForDefeat;
	public Sprite GetDisplayInScoreSceneSprite() => displayInScoreSceneSprite;
}