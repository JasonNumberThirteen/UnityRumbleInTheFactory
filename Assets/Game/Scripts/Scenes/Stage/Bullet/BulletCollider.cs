using UnityEngine;

public class BulletCollider : MonoBehaviour
{
	public GameObject splatterEffect;
	public string[] excludedTags;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		foreach (string et in excludedTags)
		{
			if(collider.CompareTag(et))
			{
				return;
			}
		}

		if(collider.CompareTag("Nuke"))
		{
			NukeRenderer nr = collider.gameObject.GetComponent<NukeRenderer>();

			if(nr != null)
			{
				nr.ChangeToDestroyedState();
				Destroy(nr);
			}
			else
			{
				return;
			}
		}

		if(collider.CompareTag("Bricks"))
		{
			Destroy(collider.gameObject);
		}

		EntityExploder ee = collider.gameObject.GetComponent<EntityExploder>();

		if(ee != null)
		{
			ee.Explode();
		}
		
		Instantiate(splatterEffect, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}