using UnityEngine;

public class EntityMovement : MonoBehaviour, IUpgradeableByRobotRank
{
	[Min(0f)] public float movementSpeed = 5f;

	public Vector2 Direction {get; set;}

	protected Rigidbody2D rb2D;

	public bool DirectionIsZero() => Direction == Vector2.zero;
	public virtual void UpdateValuesUpgradeableByPlayerRobotRank(PlayerRobotRank rank) => movementSpeed = rank.GetMovementSpeed();
	protected virtual void Awake() => rb2D = GetComponent<Rigidbody2D>();
	protected virtual void FixedUpdate() => Move();

	protected virtual void Move()
	{
		float speed = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + Direction*speed);
	}
}