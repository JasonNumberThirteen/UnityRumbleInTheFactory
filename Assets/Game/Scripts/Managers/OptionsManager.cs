using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

	public void RegisterToOptionListeners(bool register, OptionType optionType, UnityAction onSelect, UnityAction onSubmit)
	{
		if(!optionsDictionary.ContainsKey(optionType))
		{
			return;
		}

		var option = optionsDictionary[optionType];

		if(register)
		{
			option.onSelect.AddListener(onSelect);
			option.onSubmit.AddListener(onSubmit);
		}
		else
		{
			option.onSelect.RemoveListener(onSelect);
			option.onSubmit.RemoveListener(onSubmit);
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