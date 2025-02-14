using UnityEngine;

[DefaultExecutionOrder(-200)]
public class WebGLGameObjectActivationController : MonoBehaviour
{
#if UNITY_WEBGL
	private void Awake()
	{
		gameObject.SetActive(false);
	}
#endif
}