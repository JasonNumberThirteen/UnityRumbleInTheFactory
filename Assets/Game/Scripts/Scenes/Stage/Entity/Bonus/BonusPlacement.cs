using Random = UnityEngine.Random;
using UnityEngine;

public class BonusPlacement : MonoBehaviour
{
	public Vector2 bottomLeftArea, topRightArea;
	[Min(0.01f)] public float gridSize = 0.5f;
	
	private void Start() => RandomisePosition();
	private void RandomisePosition() => transform.position = RandomPosition();

	private Vector2 RandomPosition()
	{
		float x = Random.Range(bottomLeftArea.x, topRightArea.x);
		float y = Random.Range(bottomLeftArea.y, topRightArea.y);
		float gridX = Mathf.Round(x / gridSize)*gridSize;
		float gridY = Mathf.Round(y / gridSize)*gridSize;

		return new Vector2(gridX, gridY);
	}
}