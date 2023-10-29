using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
	private Dictionary<string, string> levelInfo = new Dictionary<string, string>()
	{
		{"rockyforest", "����������� �������� ������ � ������� ������!"},
		{"sandyvalley", "����������� ������� ��������-����� � ������� ������!"},
		{"lavaland", "����������� ���� � ������� ������!"}
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
