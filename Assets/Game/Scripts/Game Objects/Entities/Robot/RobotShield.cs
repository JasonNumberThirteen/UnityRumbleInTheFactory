using UnityEngine;

[RequireComponent(typeof(Timer), typeof(ShieldGameVisualEffect))]
public class RobotShield : MonoBehaviour
{
	[SerializeField] private bool activateOnStart;

	private Timer timer;
	private ShieldGameVisualEffect shieldGameVisualEffect;

	public bool IsActive() => gameObject.activeSelf;

	public void ActivateShield(float duration)
	{
		timer.duration = duration;

		timer.ResetTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		shieldGameVisualEffect = GetComponent<ShieldGameVisualEffect>();
	}

	private void Start()
	{
		shieldGameVisualEffect.SetActive(activateOnStart);
	}
}