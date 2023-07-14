using UnityEngine;

public class FortressMetalTransformation : MonoBehaviour
{
	public GameObject bricksTile;

	public void TransformSelf()
	{
		Instantiate(bricksTile, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}