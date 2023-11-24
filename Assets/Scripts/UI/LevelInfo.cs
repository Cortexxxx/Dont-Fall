using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
	private Dictionary<string, string> levelInfo = new Dictionary<string, string>()
	{
		{"rockyforest", "В окрестностях произошёл обвал с гор. Остерегайся падающих камней!"},
		{"sandyvalley", "Сегодня слишком ветренно. Берегись летящих перекати-полей"},
		{"lavaland", "Неподалёку началось извержение вулкана. Остререгайся лавовых сфер!"},
		{"snowyhills", "Берегись! Эти турели стреляют снежками!"},
		{"cityflow", "Эти водители хотят тебя сбить. Будь осторожен! "}
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
	}
}
