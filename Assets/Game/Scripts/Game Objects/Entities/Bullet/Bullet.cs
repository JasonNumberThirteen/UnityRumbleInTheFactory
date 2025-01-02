using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class Bullet : MonoBehaviour
{
	[SerializeField, Min(1)] private int damage = 1;
	[SerializeField, Min(0f)] private float initialMovementSpeed = 6.5f;
	[SerializeField] private bool canDestroyMetal;

	private EntityMovement entityMovement;
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
		entityMovement.SetMovementSpeed(movementSpeed);
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
		entityMovement = GetComponent<EntityMovement>();
	}

	private void Start()
	{
		if(Mathf.Approximately(entityMovement.GetMovementSpeed(), 0f))
		{
			SetMovementSpeed(initialMovementSpeed);
		}
	}
}