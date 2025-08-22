using UnityEngine;

[DefaultExecutionOrder(-10)]
public class BootUIManager : UIManager
{
	private void Awake()
	{
		CursorMethods.SetCursorVisible(false);
	}
}