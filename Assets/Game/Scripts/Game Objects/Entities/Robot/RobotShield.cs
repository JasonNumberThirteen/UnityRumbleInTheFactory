using UnityEngine;

[RequireComponent(typeof(Timer), typeof(ShieldVisualEffect))]
public class RobotShield : MonoBehaviour
{
	[SerializeField] private bool activateOnStart;

	private Timer timer;
	private ShieldVisualEffect shieldVisualEffect;

	public bool IsActive() => gameObject.activeSelf;

	public void ActivateShield(float duration)
	{
		timer.duration = duration;

		timer.ResetTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		shieldVisualEffect = GetComponent<ShieldVisualEffect>();
	}

	private void Start()
	{
		shieldVisualEffect.SetActive(activateOnStart);
	}
}