using System;
using UnityEngine;

[Serializable]
public class StageData
{
	[SerializeField] private int[] tileIndexes;
	[SerializeField] private string[] enemyTypes;

	public int[] GetTileIndexes() => tileIndexes;
	public string[] GetEnemyTypes() => enemyTypes;
}