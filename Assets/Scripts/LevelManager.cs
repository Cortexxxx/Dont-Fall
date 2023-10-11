using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;
	public Vector3[] platformSizes;
	[SerializeField] private GameObject[] objectsToDestoy;
	[HideInInspector] public bool isCompleted = false;
	public enum Difficulties
	{
		Easy,
		Medium,
		Hard,
		Infinity
	}
	public Difficulties difficulty;
	private void Start()
	{
		isCompleted = false;
	}
	private void Awake()
	{
		// Singleton
		if (Instance == null)
		{
			Instance = this;	
		}
		else
		{
			Destroy(gameObject);
		}
		if (PlayerPrefs.GetString("difficulty") == "easy")
		{
			difficulty = Difficulties.Easy;
		}
		else if (PlayerPrefs.GetString("difficulty") == "medium")
		{
			difficulty = Difficulties.Medium;
		}
		else if(PlayerPrefs.GetString("difficulty") == "hard")
		{
			difficulty = Difficulties.Hard;
		}
		else
		{
			throw new Exception("There is no such difficulty");
		}
	}

	void OnEnable()
	{
		Debug.Log("OnEnable called");
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		Debug.Log(SceneManager.GetActiveScene().name);
		if (SceneManager.GetActiveScene().name == "menu")
		{

			foreach (var item in objectsToDestoy)
			{
				Destroy(item);
			}
		}
	}
}
