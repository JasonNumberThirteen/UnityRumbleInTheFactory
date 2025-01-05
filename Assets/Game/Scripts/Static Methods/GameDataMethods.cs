using UnityEngine;

public class GameDataMethods : MonoBehaviour
{
	public static bool NoStagesFound(GameData gameData) => gameData != null && gameData.NoStagesFound();
}