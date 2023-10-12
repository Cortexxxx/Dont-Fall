using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
	[SerializeField] private GameObject[] dontDestroyObjects;
	private void Awake()
	{
		foreach (GameObject obj in dontDestroyObjects)
		{
			DontDestroyOnLoad(obj);
		}
		SceneManager.LoadScene(PlayerPrefs.GetString("levelToLaunch"));
	}
}
