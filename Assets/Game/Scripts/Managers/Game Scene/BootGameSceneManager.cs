public class BootGameSceneManager : GameSceneManager
{
	private void Awake()
	{
		InitSaveableDataIfPossible();	
		LoadSceneByName(MAIN_MENU_SCENE_NAME);
	}

	private void InitSaveableDataIfPossible()
	{
		var saveableDataInitialiser = ObjectMethods.FindComponentOfType<SaveableDataInitialiser>();

		if(saveableDataInitialiser != null)
		{
			saveableDataInitialiser.InitSaveableData();
		}
	}
}