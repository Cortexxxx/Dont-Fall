using UnityEngine;

public class UIContainer : MonoBehaviour
{
	public static UIContainer Instance;
	public GameObject coinText;	
	public GameObject startGameTimer;
	public GameObject timer;
	private void Awake()
	{
		// Singleton
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
