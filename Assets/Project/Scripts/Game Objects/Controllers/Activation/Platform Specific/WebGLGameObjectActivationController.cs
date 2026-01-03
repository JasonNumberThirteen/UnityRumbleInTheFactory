public class WebGLGameObjectActivationController : GameObjectActivationController
{
	protected override bool GOShouldBeActive()
	{
#if UNITY_WEBGL
		return false;
#else
		return true;
#endif
	}
}