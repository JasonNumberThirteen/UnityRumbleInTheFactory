using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour, IUpgradeableByRobotRank
{
	public Vector2 CurrentMovementDirection {get; set;}

	[SerializeField, Min(0f)] private float movementSpeed = 5f;

	protected Rigidbody2D rb2D;

	public float GetMovementSpeed() => movementSpeed;
	public bool CurrentMovementDirectionIsNone() => CurrentMovementDirection == Vector2.zero;

	public virtual void UpdateValuesUpgradeableByRobotRank(RobotRank robotRank)
	{
		movementSpeed = robotRank.GetMovementSpeed();
	}

	public void SetMovementSpeed(float movementSpeed)
	{
		this.movementSpeed = movementSpeed;
	}

	protected virtual void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	protected virtual void FixedUpdate()
	{
		var speed = movementSpeed*Time.fixedDeltaTime;

		rb2D.MovePosition(rb2D.position + CurrentMovementDirection*speed);
	}
}