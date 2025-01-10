using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Data")]
public class EnemyData : RobotData<RobotRank>
{
	[SerializeField] private GameObject prefab;
	[SerializeField, Min(0)] private int pointsForDefeat;
	[SerializeField] private Sprite displayInScoreSceneSprite;

	public GameObject GetPrefab() => prefab;
	public int GetPointsForDefeat() => pointsForDefeat;
	public Sprite GetDisplayInScoreSceneSprite() => displayInScoreSceneSprite;
}