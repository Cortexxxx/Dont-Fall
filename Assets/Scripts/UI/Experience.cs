using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Experience : MonoBehaviour
{
	[SerializeField] private int baseLevelMulti = 50;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private UnityEngine.UI.Slider xpSlider;
	[SerializeField] private TextMeshProUGUI xpText;
	private void Start()
	{
		if (!PlayerPrefs.HasKey("xp"))
		{
			PlayerPrefs.SetInt("xp", 0);
		}
		if (!PlayerPrefs.HasKey("currentxp"))
		{
			PlayerPrefs.SetInt("currentxp", 0);
		}
		if (!PlayerPrefs.HasKey("level"))
		{
			PlayerPrefs.SetInt("level", 0);
		}
		int xp = PlayerPrefs.GetInt("xp");
		levelText.text = Player.GetLevel(PlayerPrefs.GetInt("xp")).ToString();
		int xpForNextLevel = GetXP(Player.GetLevel(PlayerPrefs.GetInt("xp")) + 1);
		int xpForPreviousLevel = GetXP(Player.GetLevel(PlayerPrefs.GetInt("xp")));
		double sliderValue = (Convert.ToDouble(PlayerPrefs.GetInt("xp")) - Convert.ToDouble(xpForPreviousLevel)) / (Convert.ToDouble(xpForNextLevel) - Convert.ToDouble(xpForPreviousLevel));

		xpSlider.value = (float)sliderValue;
		xpText.text = $"{xp - xpForPreviousLevel} XP / {xpForNextLevel - xpForPreviousLevel}";

	}


	private int GetXP(int level)
	{
		int xp = 0;
		for (int i = 0; i < level; i++)
		{
			xp += 50 * i;
		}
		return xp;
	}
}
