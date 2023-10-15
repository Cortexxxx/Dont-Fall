using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
	private Dictionary<string, string> levelInfo = new Dictionary<string, string>()
	{
		{"rockyforest", "ќстерегайс€ падающих камней и собирай монеты!"},
		{"sandyvalley", "ќстерегайс€ лет€щих перекати-полей и собирай монеты!"}
	};
	private void Start()
	{
		GetComponentInChildren<TextMeshProUGUI>().text = levelInfo[SceneManager.GetActiveScene().name];
	}
}
