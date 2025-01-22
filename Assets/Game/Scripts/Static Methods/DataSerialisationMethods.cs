using System.IO;
using UnityEngine;

public static class DataSerialisationMethods
{
	private static readonly string DATA_FILE_EXTENSION = "json";
	
	public static void SaveData(string filename, object @object)
	{
		var filePath = GetFilePath(filename);
		var data = JsonUtility.ToJson(@object);
		
		File.WriteAllText(filePath, data);
	}

	public static void LoadDataIfPossible(string filename, object @object)
	{
		var filePath = GetFilePath(filename);

		if(!File.Exists(filePath))
		{
			return;
		}

		var data = File.ReadAllText(filePath);

		JsonUtility.FromJsonOverwrite(data, @object);
	}

	private static string GetFilePath(string filename) => Path.Combine(Application.persistentDataPath, $"{filename}.{DATA_FILE_EXTENSION}");
}