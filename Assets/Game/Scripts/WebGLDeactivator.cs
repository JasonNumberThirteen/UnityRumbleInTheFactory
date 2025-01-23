using UnityEngine;

[DefaultExecutionOrder(-200)]
public class WebGLDeactivator : MonoBehaviour
{
#if UNITY_WEBGL
	private void Awake()
	{
		gameObject.SetActive(false);
	}
#endif
}