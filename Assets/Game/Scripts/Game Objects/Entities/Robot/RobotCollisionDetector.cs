using UnityEngine;

public class RobotCollisionDetector : MonoBehaviour
{
	public LayerMask detectionLayers;
	
	private Collider2D c2D;

	public Collider2D OverlapBox() => Physics2D.OverlapBox(c2D.bounds.center, c2D.bounds.size, 0f, detectionLayers);
	public Collider2D[] OverlapBoxAll() => Physics2D.OverlapBoxAll(c2D.bounds.center, c2D.bounds.size, 0f, detectionLayers);

	private void Awake() => c2D = GetComponent<Collider2D>();
}