using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Players List Data")]
public class PlayersListData : ScriptableObject
{
	[SerializeField] private List<PlayerData> playersData = new();

	public bool Any(Func<PlayerData, bool> func) => playersData.Any(func);

	public void ForEach(Action<PlayerData> action)
	{
		playersData.ForEach(action);
	}
}