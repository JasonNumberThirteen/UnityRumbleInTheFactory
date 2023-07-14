using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Data")]
public class GameData : ScriptableObject
{
	public int stage = 1, highScore = 20000;
}