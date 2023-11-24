using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
	[SerializeField] AudioClip clip;


	private void Update()
	{
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
		Fade.Instance.FadeFinishLevelDelay(delay);
	}
}
