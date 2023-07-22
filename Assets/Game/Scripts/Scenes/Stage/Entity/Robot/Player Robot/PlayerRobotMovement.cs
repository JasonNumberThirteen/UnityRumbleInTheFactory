using UnityEngine;

public class PlayerRobotMovement : EntityMovement
{
	public LayerMask movementFreezeLayers;
	public Collider2D collisionDetector;
	
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

	private void Update()
	{
		AdjustCollisionDetectorRotation();
	}

	private void AdjustCollisionDetectorRotation()
	{
		Transform collisionDetectorTransform = collisionDetector.gameObject.transform;
		
		if(Direction == Vector2.up)
		{
			collisionDetectorTransform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else if(Direction == Vector2.down)
		{
			collisionDetectorTransform.rotation = Quaternion.Euler(0, 0, 180);
		}
		else if(Direction == Vector2.left)
		{
			collisionDetectorTransform.rotation = Quaternion.Euler(0, 0, 90);
		}
		else if(Direction == Vector2.right)
		{
			collisionDetectorTransform.rotation = Quaternion.Euler(0, 0, 270);
		}
	}
	
	protected override void FixedUpdate()
	{
		Direction = MovementVector();
		LockMovementWhenHitObject();
		base.FixedUpdate();
	}

	private void LockMovementWhenHitObject()
	{
		Collider2D c = Physics2D.OverlapBox(collisionDetector.bounds.center, collisionDetector.bounds.size, 0f, movementFreezeLayers);

		if(c != null)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeAll;

			Debug.Log(c.gameObject.tag);
		}
		else if(rb2D.constraints != RigidbodyConstraints2D.FreezeRotation)
		{
			rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}