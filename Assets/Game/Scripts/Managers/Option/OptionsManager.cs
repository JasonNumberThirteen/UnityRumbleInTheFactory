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

	public void RegisterToOptionListeners(bool register, OptionType optionType, UnityAction<Option> onOptionWasSelected, UnityAction<Option> onOptionWasSubmitted)
	{
		OperateOnOptionIfExists(optionType, option =>
		{
			if(register)
			{
				option.optionWasSelectedEvent.AddListener(onOptionWasSelected);
				option.optionWasSubmittedEvent.AddListener(onOptionWasSubmitted);
			}
			else
			{
				option.optionWasSelectedEvent.RemoveListener(onOptionWasSelected);
				option.optionWasSubmittedEvent.RemoveListener(onOptionWasSubmitted);
			}
		});
	}

	private void Awake()
	{
		var options = ObjectMethods.FindComponentsOfType<Option>(false);

		options.ForEach(option => optionsByOptionType.Add(option.GetOptionType(), option));
	}

	private void OperateOnOptionIfExists(OptionType optionType, Action<Option> action)
	{
		if(optionsByOptionType.TryGetValue(optionType, out var option))
		{
			action(option);
		}
	}
}