using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class OptionsManager : MonoBehaviour
{
	private readonly Dictionary<OptionType, Option> optionsByOptionType = new();

	public int GetNumberOfOptions() => optionsByOptionType.Count;

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

		var option = optionsByOptionType[optionType];

		if(register)
		{
			option.optionSelectedEvent.AddListener(onSelect);
			option.optionSubmittedEvent.AddListener(onSubmit);
		}
		else
		{
			option.optionSelectedEvent.RemoveListener(onSelect);
			option.optionSubmittedEvent.RemoveListener(onSubmit);
		}
	}

	private void Awake()
	{
		var options = ObjectMethods.FindComponentsOfType<Option>();

		foreach (var option in options)
		{
			optionsByOptionType.Add(option.GetOptionType(), option);
		}
	}

	private void OperateOnOptionIfExists(OptionType optionType, Action<Option> action)
	{
		if(OptionByGivenTypeExists(optionType))
		{
			action(optionsByOptionType[optionType]);
		}
	}

	private bool OptionByGivenTypeExists(OptionType optionType) => optionsByOptionType.ContainsKey(optionType);
}