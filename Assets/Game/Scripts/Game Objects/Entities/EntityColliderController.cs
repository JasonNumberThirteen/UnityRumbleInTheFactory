using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(EntityMovementController))]
public class EntityColliderController : MonoBehaviour
{
	private BoxCollider2D boxCollider2D;
	private EntityMovementController entityMovementController;
	private Vector2 currentColliderSize;

	private void Awake()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
		entityMovementController = GetComponent<EntityMovementController>();
		currentColliderSize = boxCollider2D.bounds.size;

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			entityMovementController.movementDirectionWasChangedEvent.AddListener(OnMovementDirectionWasChanged);
		}
		else
		{
			entityMovementController.movementDirectionWasChangedEvent.RemoveListener(OnMovementDirectionWasChanged);
		}
	}

	private void OnMovementDirectionWasChanged(Vector2 movementVector)
	{
		var horizontalDirections = new List<Vector2>{Vector2.left, Vector2.right};
		var verticalDirections = new List<Vector2>{Vector2.up, Vector2.down};
		
		if(horizontalDirections.Contains(movementVector))
		{
			UpdateColliderSizeIfNeeded(new Vector2(currentColliderSize.y, currentColliderSize.x));
		}
		else if(verticalDirections.Contains(movementVector))
		{
			UpdateColliderSizeIfNeeded(currentColliderSize);
		}
	}

	private void UpdateColliderSizeIfNeeded(Vector2 colliderSize)
	{
		if(boxCollider2D.size != colliderSize)
		{
			boxCollider2D.size = colliderSize;
		}
	}
}