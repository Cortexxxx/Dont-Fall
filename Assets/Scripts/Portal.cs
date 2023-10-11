using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] AudioClip clip;
	private void Update()
	{
		// Portal appear
		if (LevelManager.Instance.isCompleted == true)
		{
			GetComponent<Animator>().SetTrigger("Activate");
			GetComponent<AudioSource>().PlayOneShot(clip);
		}
	}
}
