using UnityEngine;

public class PlayerRobotMovement : EntityMovement
{
	public RobotCollisionDetector collisionDetector;
	
	public bool IsSliding {get; set;}

	private PlayerRobotInput input;

	public Vector2 MovementVector()
	{
		if(IsSliding)
		{
			return input.LastMovementVector;
		}
		
		int x = Mathf.RoundToInt(input.MovementVector.x);
		int y = Mathf.RoundToInt(input.MovementVector.y);

		if(Mathf.Abs(x) > Mathf.Abs(y))
		{
			y = 0;
		}
		else
		{
			x = 0;
		}

		return new Vector2(x, y);
	}

	protected override void Awake()
	{
		base.Awake();

		input = GetComponent<PlayerRobotInput>();
	}

	protected override void FixedUpdate()
	{
		Direction = MovementVector();

		LockMovementWhenHitObject();
		base.FixedUpdate();
	}

	private void Update() => collisionDetector.AdjustRotation(Direction);

	private void LockMovementWhenHitObject()
	{
		if(collisionDetector.OverlapBox() != null)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		else if(rb2D.constraints != RigidbodyConstraints2D.FreezeRotation)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}