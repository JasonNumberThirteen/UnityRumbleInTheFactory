using UnityEngine;

public class TwoPlayerModeGameObjectActivationController : GameObjectActivationController
{
	[SerializeField] private GameData gameData;
	
	protected override bool GOShouldBeActive() => GOActivationStateCanBeChanged() && gameData.SelectedTwoPlayerMode;
	protected override bool GOActivationStateCanBeChanged() => GameDataMethods.GameDataIsDefined(gameData);
}