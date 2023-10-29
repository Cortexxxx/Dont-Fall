using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
	private Dictionary<string, string> levelInfo = new Dictionary<string, string>()
	{
		{"rockyforest", "Остерегайся падающих камней и собирай монеты!"},
		{"sandyvalley", "Остерегайся летящих перекати-полей и собирай монеты!"},
		{"lavaland", "Остерегайся лавы и собирай монеты!"}
	};
	private void Start()
	{
		GetComponentInChildren<TextMeshProUGUI>().text = levelInfo[SceneManager.GetActiveScene().name];
		Debug.Log("start");

		StartCoroutine(SetInactive());
	}
	private IEnumerator SetInactive()
	{
		yield return new WaitForSeconds(5);
		gameObject.SetActive(false);
		Debug.Log("setinactive");
	}
}
