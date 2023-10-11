using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
	private int levelToBuy = 0;
	private int moneyToBuy = 0;
	private AudioSource audioSource;
	[SerializeField] private ChooseButton chooseButton;
	[SerializeField] private TextMeshProUGUI moneyText;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private AudioClip blockedSound;
	[SerializeField] private AudioClip getSound;
	[SerializeField] private GameObject info;
	[SerializeField] private CoinsDisplay coinsDisplay;
	private int GetLevel() {
		int level = LevelSelector.currentlevelIndex;
		if (chooseButton.difficultyTag == "easy")
		{
			level += 1;
		} else if (chooseButton.difficultyTag == "medium")
		{
			level += 2;
		} else if (chooseButton.difficultyTag == "hard")
		{
			level += 3;
		}
		return level;
	}
	private int GetMoney()
	{
		int level = LevelSelector.currentlevelIndex * 10;
		if (chooseButton.difficultyTag == "easy")
		{
			level += 1;
		}
		else if (chooseButton.difficultyTag == "medium")
		{
			level += 2;
		}
		else if (chooseButton.difficultyTag == "hard")
		{
			level += 3;
		}
		return level * 5;
	}
	private void OnEnable()
	{

		moneyText.text = GetMoney().ToString();
		levelText.text = GetLevel().ToString();
	}
	private void Start()
	{
		audioSource = chooseButton.GetComponent<AudioSource>();
	}
	public void BuyLevel()
	{
		if (PlayerPrefs.GetInt("Coins") >= GetMoney() && Player.GetLevel() >= GetLevel())
		{
			PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - GetMoney());
			SaveGame.levels[LevelSelector.currentlevelName + chooseButton.difficultyTag] = true;
			coinsDisplay.Refresh();
			audioSource.clip = getSound;
			info.SetActive(false);
			Debug.Log(LevelSelector.currentlevelName + chooseButton.difficultyTag);
		}
		else
		{
			Debug.Log("Can't buy");
			audioSource.clip = blockedSound;
		}
		audioSource.Play();
	}
}
