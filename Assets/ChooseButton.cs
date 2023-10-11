using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour
{
	[SerializeField] private GameObject unlockWindow;
	public string difficultyTag;
	[SerializeField] private GameObject[] lockedObjects;
	private bool unlocked;
	void OnMouseOver()
	{
		Debug.Log("Mouse is over GameObject.");
	}
	public void Refresh()
	{
		if (SaveGame.levels[LevelSelector.currentlevelName + difficultyTag])
		{
			foreach (GameObject go in lockedObjects)
			{
				go.SetActive(false);
				GetComponent<EventTrigger>().enabled = false;
				GetComponent<Button>().enabled = true;
			}
		}
		else
		{
			unlocked = true;
			foreach (GameObject go in lockedObjects)
			{
				go.SetActive(true);
				GetComponent<EventTrigger>().enabled = true;
				GetComponent<Button>().enabled = false;
			}
		}
	}

	private void Update()
	{
		if (LevelSelector.currentlevelName != null)
		{
			Refresh();
		}
	}
}
