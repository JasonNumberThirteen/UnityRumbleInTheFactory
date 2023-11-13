using UnityEngine;

public class PlayerRobotShield : MonoBehaviour
{
	public string shieldTag;
	
	public Timer ShieldTimer {get; private set;}

	private void Awake() => ShieldTimer = GetComponentInChildren<Timer>();
}