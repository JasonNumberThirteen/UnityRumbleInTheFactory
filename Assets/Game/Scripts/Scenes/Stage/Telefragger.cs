using UnityEngine;

public class Telefragger : MonoBehaviour
{
	[SerializeField, Min(0.01f)] private float radius = 0.3f;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color sphereGizmosColor = Color.black;
	
	public void TelefragGOsWithinRadius()
	{
		var colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

		foreach (var collider in colliders)
		{
			TelefragGO(collider.gameObject);
		}
	}

	private void TelefragGO(GameObject go)
	{
		if(go.TryGetComponent(out RobotHealth robotHealth))
		{
			robotHealth.TakeDamage(gameObject, int.MaxValue);
		}
		else if(go.TryGetComponent(out EntityExploder entityExploder))
		{
			entityExploder.TriggerExplosion();
		}
		else
		{
			Destroy(go);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = sphereGizmosColor;

		Gizmos.DrawWireSphere(transform.position, radius);
	}
}