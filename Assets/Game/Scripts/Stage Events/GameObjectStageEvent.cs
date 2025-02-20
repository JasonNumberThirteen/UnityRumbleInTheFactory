using UnityEngine;

public class GameObjectStageEvent : StageEvent
{
	private readonly GameObject go;

	public GameObjectStageEvent(StageEventType stageEventType, GameObject go) : base(stageEventType)
	{
		this.go = go;
	}

	public GameObject GetGO() => go;
}