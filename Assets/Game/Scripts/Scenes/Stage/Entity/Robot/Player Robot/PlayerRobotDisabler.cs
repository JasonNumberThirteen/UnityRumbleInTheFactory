using UnityEngine;

public class PlayerRobotDisabler : MonoBehaviour
{
	public void DisableYourself()
	{
		if(TryGetComponent(out EntityMovement em))
		{
			em.CurrentMovementDirection = Vector2.zero;

			Destroy(em);
		}

		if(TryGetComponent(out RobotShoot rs))
		{
			Destroy(rs);
		}

		if(TryGetComponent(out PlayerRobotInput pri))
		{
			Destroy(pri);
		}
	}
}