using TMPro;
using UnityEngine;

public class CoinsDisplay : MonoBehaviour
{
	private void Start()
	{
		Refresh();
	}
	public void Refresh()
	{
		GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Coins").ToString();
	}
}
