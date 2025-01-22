using UnityEngine;
using UnityEngine.UI;

public class DefeatedEnemyRobotTypeIntCounterPanelUI : IntCounterPanelUI
{
	private Image image;
	
	public void SetSprite(Sprite sprite)
	{
		if(image != null)
		{
			image.sprite = sprite;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		
		image = GetComponentInChildren<Image>();
	}
}