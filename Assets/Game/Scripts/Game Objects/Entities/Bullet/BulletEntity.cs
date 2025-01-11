using UnityEngine;

[RequireComponent(typeof(EntityMovementController))]
public class BulletEntity : MonoBehaviour
{
	private int damage;
	private bool canDestroyMetal;
	private EntityMovementController entityMovementController;
	private GameObject parent;

	public int GetDamage() => damage;
	public bool CanDestroyMetal() => canDestroyMetal;
	public GameObject GetParent() => parent;
	public Vector2 GetMovementDirection() => entityMovementController.CurrentMovementDirection;

	public void Setup(BulletStats bulletStats, GameObject parentGO, Vector2 movementDirection)
	{
		SetParent(parentGO);
		entityMovementController.SetMovementSpeed(bulletStats.GetBulletSpeed());
		
		damage = bulletStats.GetDamage();
		canDestroyMetal = bulletStats.CanDestroyMetal();
		entityMovementController.CurrentMovementDirection = movementDirection;
	}

	public void SetParent(GameObject parent)
	{
		this.parent = parent;
	}

	private void Awake()
	{
		entityMovementController = GetComponent<EntityMovementController>();
	}
}