using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
	[SerializeField] AudioClip clip;
	bool needToBeClosedInNextScene = false;
	private void Start()
	{
		SceneManager.sceneLoaded += CheckFade;
	}

	private void CheckFade(Scene arg0, LoadSceneMode arg1)
	{
		if (needToBeClosedInNextScene)
		{
			if (Fade.Instance.fade.GetComponent<Animator>().GetBool("Activate"))
			{
				Fade.Instance.fade.GetComponent<Animator>().SetBool("Activate", false);
			}
			needToBeClosedInNextScene = false;
		}
	}

	private void Update()
	{
		// Portal appear
		if (LevelManager.Instance.isCompleted == true)
		{
			GetComponent<Animator>().SetTrigger("Activate");
			GetComponent<AudioSource>().PlayOneShot(clip);
		}
	}

	public void FinishLevel()
	{
		Animator fadeAnimator = Fade.Instance.fade.GetComponent<Animator>();
		fadeAnimator.SetBool("Activate", true);
		float delay = fadeAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
		StartCoroutine(FadeDelay(delay));
	}

	private IEnumerator FadeDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		needToBeClosedInNextScene = true;
		int xpForLevel = ((int)LevelManager.Instance.difficulty + 1) * (LevelSelector.levels.FindFirstKeyByValue(LevelSelector.currentlevelName) + 1) * 10 + Player.Instance.Coins;
		PlayerPrefs.SetInt("xp", PlayerPrefs.GetInt("xp") + xpForLevel);
		SceneManager.LoadScene("menu");
	}
}
