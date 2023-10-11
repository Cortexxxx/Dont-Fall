using UnityEngine;

public class UIContainer : MonoBehaviour
{
	public static UIContainer Instance;
	public GameObject coinText;	
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
