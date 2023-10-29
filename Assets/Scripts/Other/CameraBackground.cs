using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBackground : MonoBehaviour
{
	[SerializeField] private Color netherBackgroundColor;
	[SerializeField] private Color skyBackgroundColor;
	private void Start()
	{
		Set();
		SceneManager.sceneLoaded += SetBackground;
	}

	private void SetBackground(Scene arg0, LoadSceneMode arg1)
	{
		Set();
	}

	private void Set()
	{
		Debug.Log(SceneManager.GetActiveScene().name);
		if (SceneManager.GetActiveScene().name == "lavaland")
		{
			GetComponent<Camera>().backgroundColor = netherBackgroundColor;
		}
		else
		{
			GetComponent<Camera>().backgroundColor = skyBackgroundColor;
		}
	}
}
