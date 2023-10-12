using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
	public static Fade Instance;
	public GameObject fade;
	public FadeColorPalette[] colorPalettes;
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
	private void SetFadeState(Scene scene, LoadSceneMode mode)
	{
		if (fade.GetComponent<Animator>().GetBool("Activate") && SceneManager.GetActiveScene().name != "menu")
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
}
