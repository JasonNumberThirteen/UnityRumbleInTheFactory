using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
	public GameObject prefab, bonusTypePrefab;
	[Min(0)] public int score;
	public Sprite sprite;
}