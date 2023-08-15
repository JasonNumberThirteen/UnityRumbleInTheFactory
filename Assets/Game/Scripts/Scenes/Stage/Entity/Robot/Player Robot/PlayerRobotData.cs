using UnityEngine;

public class PlayerRobotData : MonoBehaviour
{
	public PlayerData Data {get; private set;}
	
	[SerializeField] private PlayerData data;

	private void Awake() => Data = data;
}