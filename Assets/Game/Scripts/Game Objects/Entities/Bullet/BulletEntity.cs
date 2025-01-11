using UnityEngine;

[RequireComponent(typeof(EntityMovementController))]
public class BulletEntity : MonoBehaviour
{
	private int damage;
	private bool canDestroyMetal;
	private EntityMovementController entityMovementController;
	private GameObject parentGO;

	public int GetDamage() => damage;
	public bool CanDestroyMetal() => canDestroyMetal;
	public GameObject GetParentGO() => parentGO;
	public Vector2 GetMovementDirection() => entityMovementController.CurrentMovementDirection;

	public void Setup(BulletStats bulletStats, GameObject parentGO, Vector2 movementDirection)
	{
		entityMovementController.SetMovementSpeed(bulletStats.GetBulletSpeed());
		
		damage = bulletStats.GetDamage();
		canDestroyMetal = bulletStats.CanDestroyMetal();
		this.parentGO = parentGO;
		entityMovementController.CurrentMovementDirection = movementDirection;
	}

	private void Awake()
	{
		entityMovementController = GetComponent<EntityMovementController>();
	}
}