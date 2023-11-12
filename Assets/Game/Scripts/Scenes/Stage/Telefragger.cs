using UnityEngine;

public class Telefragger : MonoBehaviour
{
	[Min(0.01f)] public float distance = 0.1f;
	public LayerMask layerMask;
	
	public void Telefrag()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distance, layerMask);

		foreach (Collider2D c in colliders)
		{
			GameObject go = c.gameObject;
			
			if(go.TryGetComponent(out RobotHealth rh))
			{
				rh.TakeDamage(gameObject, int.MaxValue);
			}
			else if(go.TryGetComponent(out EntityExploder ee))
			{
				ee.Explode();
			}
			else
			{
				Destroy(go);
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.black;

		Gizmos.DrawWireSphere(transform.position, distance);
	}
}