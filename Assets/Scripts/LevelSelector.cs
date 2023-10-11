using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
	public static int currentlevelIndex;
	public static string currentlevelName;
	[SerializeField] public GameObject[] levelPages;

	public static Dictionary<int, string> levels = new Dictionary<int, string>()
	{
		{0, "rockyforest"},
		{1, "sandyvalley"}
	};
	private void Start()
	{
		currentlevelIndex = 0;
		LevelSelector.currentlevelName = levels[currentlevelIndex];
	}
	private void OnEnable()
	{
		GameLoader.LoadLevels();
	}
	private void OnDisable()
	{
		SaveGame.SaveLevels();
	}

	public void SwitchLevelPage(bool isRightbuttonClicked = false)
	{
		levelPages[currentlevelIndex].SetActive(false);
		if (isRightbuttonClicked)
		{
			// If right arrow pressed
			if (currentlevelIndex == levels.Count - 1)
			{
				currentlevelIndex = 0;
			} else if (currentlevelIndex < levels.Count - 1)
			{
				currentlevelIndex++;
			}
			else
			{
				throw new Exception("Key is out of range");
			}
		}
		else 
		{
			// If left arrow pressed

			if (currentlevelIndex == 0)
			{
				currentlevelIndex = levels.Count - 1;
			}
			else if (currentlevelIndex > 0)
			{
				currentlevelIndex--;
			}
			else
			{
				throw new Exception("Key is out of range");
			}
		}
		LevelSelector.currentlevelName = levels[currentlevelIndex];
		levelPages[currentlevelIndex].SetActive(true);
	}
	public void StartLevel(string levelDifficulty)
	{
		Animator fadeAnimator = Fade.Instance.fade.GetComponent<Animator>();
		fadeAnimator.SetBool("Activate", true);
		float delay = fadeAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
		StartCoroutine(FadeDelay(delay, levelDifficulty));

	}

	private IEnumerator FadeDelay(float delay, string levelDifficulty)
	{
		yield return new WaitForSeconds(delay);
		PlayerPrefs.SetString("levelToLaunch", currentlevelName + levelDifficulty);
		PlayerPrefs.SetString("difficulty", levelDifficulty);
		SceneManager.LoadScene("transition");
	}
}
