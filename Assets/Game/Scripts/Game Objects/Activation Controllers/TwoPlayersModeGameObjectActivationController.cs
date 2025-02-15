using UnityEngine;

public class TwoPlayersModeGameObjectActivationController : GameObjectActivationController
{
	[SerializeField] private GameData gameData;
	
	protected override bool GOShouldBeActive() => GOActivationStateCanBeChanged() && gameData.SelectedTwoPlayersMode;
	protected override bool GOActivationStateCanBeChanged() => GameDataMethods.GameDataIsDefined(gameData);
}