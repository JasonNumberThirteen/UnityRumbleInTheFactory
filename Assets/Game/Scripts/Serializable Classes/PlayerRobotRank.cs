using System;
using UnityEngine;

[Serializable]
public class PlayerRobotRank : RobotRank
{
	[SerializeField, Min(1)] private int bulletsLimitAtOnce;

	public int GetBulletsLimitAtOnce() => bulletsLimitAtOnce;
}