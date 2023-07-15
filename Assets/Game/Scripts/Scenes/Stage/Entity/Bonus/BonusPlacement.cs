using Random = UnityEngine.Random;
using UnityEngine;

public class BonusPlacement : MonoBehaviour
{
	public Vector2 bottomLeftArea, topRightArea;
	
	private void Start() => RandomisePosition();
	private void RandomisePosition() => transform.position = RandomPosition();

	private Vector2 RandomPosition()
	{
		float x = Random.Range(bottomLeftArea.x, topRightArea.x);
		float y = Random.Range(bottomLeftArea.y, topRightArea.y);

		return new Vector2(x, y);
	}
}