using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
	public static Fade Instance;
	public GameObject fade;
	public FadeColorPalette[] colorPalettes;
	public bool needToBeClosedInNextScene = false;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		SceneManager.sceneLoaded += SetFadeState;
	}
	private void Start()
	{
		SceneManager.sceneLoaded += CheckFade;
	}

	private void CheckFade(Scene arg0, LoadSceneMode arg1)
	{
		Debug.Log("CHECK FADE");
		if (needToBeClosedInNextScene)
		{
			if (Instance.fade.GetComponent<Animator>().GetBool("Activate"))
			{
				Instance.fade.GetComponent<Animator>().SetBool("Activate", false);
			}
			needToBeClosedInNextScene = false;
		}
	}
	private void SetFadeState(Scene scene, LoadSceneMode mode)
	{
		if (!fade) return;
        if (SceneManager.GetActiveScene().name != "menu" && fade.GetComponent<Animator>().GetBool("Activate"))
		{
			fade.GetComponent<Animator>().SetBool("Activate", false);
		}
	}
	public void SetNewColors(int index)
	{
		Image[] rows = GetComponentsInChildren<Image>();
		for (int i = 0; i < rows.Length; i++)
		{
			rows[i].color = colorPalettes[index].colors[i];
		}
	}
	public void FadeFinishLevelDelay(float delay)
	{
		int xpForLevel = ((int)LevelManager.Instance.difficulty + 1) * (LevelSelector.levels.FindFirstKeyByValue(LevelSelector.currentlevelName) + 1) * 10 + Player.Instance.Coins;
		PlayerPrefs.SetInt("xp", PlayerPrefs.GetInt("xp") + xpForLevel);
		StartCoroutine(FadeDelay(delay));
	}
	public IEnumerator FadeDelay(float delay)
	{
		needToBeClosedInNextScene = true;
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene("menu");
	}
}
