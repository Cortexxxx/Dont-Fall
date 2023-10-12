using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;

public class GameLoader : MonoBehaviour
{
	private static string fileName = "save";
	public static void LoadLevels()
	{
		if (!File.Exists(Application.persistentDataPath + "/" + fileName + ".bin"))
		{
			SaveGame.SaveLevels();
		}
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Open(Application.persistentDataPath + "/" + fileName + ".bin", FileMode.Open);
		Dictionary<string, bool> pass = (Dictionary<string, bool>)formatter.Deserialize(saveFile);

		foreach (var k in pass.Keys)
		{
			SaveGame.levels[k] = pass[k];
		}
		saveFile.Close();
	}
}
