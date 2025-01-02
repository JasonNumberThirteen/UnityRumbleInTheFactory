using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class OptionsManager : MonoBehaviour
{
	private readonly Dictionary<OptionType, Option> optionsDictionary = new();

	public int GetNumberOfOptions() => optionsDictionary.Count;

	public void SelectOptionIfPossible(OptionType optionType)
	{
		OperateOnOptionIfExists(optionType, option => option.Select());
	}

	public void SubmitOptionIfPossible(OptionType optionType)
	{
		OperateOnOptionIfExists(optionType, option => option.Submit());
	}

	public void RegisterToOptionListeners(bool register, OptionType optionType, UnityAction onSelect, UnityAction onSubmit)
	{
		if(!OptionByGivenTypeExists(optionType))
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
		var options = FindObjectsByType<Option>(FindObjectsInactive.Include, FindObjectsSortMode.None);

		foreach (var option in options)
		{
			optionsDictionary.Add(option.GetOptionType(), option);
		}
	}

	private void OperateOnOptionIfExists(OptionType optionType, Action<Option> action)
	{
		if(OptionByGivenTypeExists(optionType))
		{
			action(optionsDictionary[optionType]);
		}
	}

	private bool OptionByGivenTypeExists(OptionType optionType) => optionsDictionary.ContainsKey(optionType);
}