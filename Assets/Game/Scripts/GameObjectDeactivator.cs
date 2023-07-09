using UnityEngine;

public class GameObjectDeactivator : MonoBehaviour
{
	private void Start() => gameObject.SetActive(false);
}