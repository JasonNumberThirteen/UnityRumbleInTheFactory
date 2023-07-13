using UnityEngine;

public class PlayerRobotShield : MonoBehaviour
{
	public Timer ShieldTimer {get; private set;}

	private void Awake() => ShieldTimer = GameObject.FindGameObjectWithTag("Shield").GetComponent<Timer>();
}