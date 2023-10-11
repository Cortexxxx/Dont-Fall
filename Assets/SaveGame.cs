using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public static class SaveGame
{
	public static Dictionary<string, bool> levels = new Dictionary<string, bool>();
	private static string fileName = "save";
	private static string[] defaultOpenedLevels =
	{
		"rockyforesteasy",
		"rockyforestmedium"
	};
	public static void SaveLevels()
	{

		if (!File.Exists(Application.persistentDataPath + "/" + fileName + ".bin"))
		{
			Debug.Log("Creating new file");
			CreateLevelsFile();
		}
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Create(Application.persistentDataPath + "/" + fileName + ".bin");
		Debug.Log(Application.persistentDataPath + "/" + fileName + ".bin");
		formatter.Serialize(saveFile, levels);
		saveFile.Close();
	}
	public static void CreateLevelsFile()
	{
		Debug.Log("Creation");
		string[] levelNames = Resources.LoadAll("Scenes/Levels/").Select(x => x.name).ToArray();
		levels.Clear();
		// Устанавливает все лвла закрытыми
		for (int i = 0; i < levelNames.Length; i++)
		{
			levels.Add(levelNames[i], false);
			/*Debug.Log(levelNames[i]);*/
		}
		// Открывает стартовые уровни
		for (int i = 0; i < defaultOpenedLevels.Length; i++)
		{
			levels[defaultOpenedLevels[i]] = true;
		}
	}
}
