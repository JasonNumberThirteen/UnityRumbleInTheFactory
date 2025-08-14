using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Robot Data")]
public class EnemyRobotData : RobotData
{
	[SerializeField] private GameObject prefab;
	[SerializeField, Min(0)] private int pointsForDefeat;
	[SerializeField] private Sprite displayInScoreSceneSprite;
	[SerializeField] private EnemyRobotRank[] ranks;

	public override RobotRank GetRankByIndex(int index) => ranks.GetElementAt(index);
	public override int GetNumberOfRanks() => ranks != null && ranks.Length > 0 ? ranks.Length : 1;

	public GameObject GetPrefab() => prefab;
	public int GetPointsForDefeat() => pointsForDefeat;
	public Sprite GetDisplayInScoreSceneSprite() => displayInScoreSceneSprite;
}