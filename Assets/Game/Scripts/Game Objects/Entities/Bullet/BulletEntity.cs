using UnityEngine;

[RequireComponent(typeof(EntityMovementController))]
public class BulletEntity : MonoBehaviour
{
	[SerializeField, Min(1)] private int damage = 1;
	[SerializeField, Min(0f)] private float initialMovementSpeed = 6.5f;
	[SerializeField] private bool canDestroyMetal;

	private EntityMovementController entityMovementController;
	private GameObject parent;

	public int GetDamage() => damage;
	public bool CanDestroyMetal() => canDestroyMetal;
	public GameObject GetParent() => parent;

	public void SetDamage(int damage)
	{
		this.damage = damage;
	}

	public void SetMovementSpeed(float movementSpeed)
	{
		entityMovementController.SetMovementSpeed(movementSpeed);
	}

	public void SetCanDestroyMetal(bool canDestroyMetal)
	{
		this.canDestroyMetal = canDestroyMetal;
	}

	public void SetParent(GameObject parent)
	{
		this.parent = parent;
	}

	private void Awake()
	{
		entityMovementController = GetComponent<EntityMovementController>();
	}

	private void Start()
	{
		if(Mathf.Approximately(entityMovementController.GetMovementSpeed(), 0f))
		{
			SetMovementSpeed(initialMovementSpeed);
		}
	}
}