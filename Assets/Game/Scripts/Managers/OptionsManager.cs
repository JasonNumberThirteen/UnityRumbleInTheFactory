using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
	private readonly Dictionary<OptionType, Option> optionsDictionary = new();

	public int GetNumberOfOptions() => optionsDictionary.Count;

	public void SelectOption(OptionType optionType)
	{
		if(optionsDictionary.ContainsKey(optionType))
		{
			optionsDictionary[optionType].Select();
		}
	}

	public void SubmitOption(OptionType optionType)
	{
		if(optionsDictionary.ContainsKey(optionType))
		{
			optionsDictionary[optionType].Submit();
		}
	}

	private void Awake()
	{
		FindOptions();
	}

	private void FindOptions()
	{
		var options = FindObjectsByType<Option>(FindObjectsInactive.Include, FindObjectsSortMode.None);

		foreach (var option in options)
		{
			optionsDictionary.Add(option.GetOptionType(), option);
		}
	}
}