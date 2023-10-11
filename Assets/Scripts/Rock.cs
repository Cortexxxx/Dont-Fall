using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour
{
	[SerializeField] private ParticleSystem particlesSystem;
	[SerializeField] private AudioClip[] clips;
	[SerializeField] private int[] fallForces;
	private void OnEnable()
	{
		GetComponent<Rigidbody>().AddForce(0,-1000 * fallForces[(int)LevelManager.Instance.difficulty] * Time.deltaTime, 0);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Crash();
		Player player = collision.collider.GetComponent<Player>();
		if (player != null)
		{
			player.Die();
		}

	}
	private IEnumerator SetInactiveAfter(float time)
	{
		yield return new WaitForSeconds(time);
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<Collider>().enabled = true;
		particlesSystem.Stop();
		particlesSystem.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}
	public void Crash()
	{

		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		particlesSystem.gameObject.SetActive(true);
		particlesSystem.Play();
		GetComponent<AudioSource>().clip = clips[Random.Range(0, clips.Length)];
		GetComponent<AudioSource>().Play();
		StartCoroutine(SetInactiveAfter(1));
	}
}
