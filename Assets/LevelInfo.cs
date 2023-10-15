using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
	private Dictionary<string, string> levelInfo = new Dictionary<string, string>()
	{
		{"rockyforest", "����������� �������� ������ � ������� ������!"},
		{"sandyvalley", "����������� ������� ��������-����� � ������� ������!"}
	};
	private void Start()
	{
		GetComponentInChildren<TextMeshProUGUI>().text = levelInfo[SceneManager.GetActiveScene().name];
	}
}
