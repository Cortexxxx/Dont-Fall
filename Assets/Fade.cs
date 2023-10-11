using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
	public static Fade Instance;
	public GameObject fade;
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
}
