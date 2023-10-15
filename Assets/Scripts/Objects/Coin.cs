using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] private ParticleSystem particlesSystem;	
	[SerializeField] private int[] coinAmmounts;
	[SerializeField] private int[] fallForces;
	bool isCollected = false;

	private void OnEnable()
	{
		GetComponent<Rigidbody>().AddForce(0, -100 * fallForces[(int)LevelManager.Instance.difficulty], 0);
	}

	public void Collect()
	{
			Player.Instance.Coins += coinAmmounts[(int)LevelManager.Instance.difficulty] + Random.Range(-1, 1) * ((int)LevelManager.Instance.difficulty + 1);
			GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
			GetComponent<AudioSource>().Play();
			Crash();
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.GetComponent<Player>())
		{
			Collect();
		}
	}

	private IEnumerator SetInactiveAfter(float time)
	{
		yield return new WaitForSeconds(time);
		GetComponentInChildren<MeshRenderer>().enabled = true;
		GetComponent<Collider>().enabled = true;
		particlesSystem.Stop();
		particlesSystem.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}
	
	public void Crash()
	{
		GetComponentInChildren<MeshRenderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		particlesSystem.gameObject.SetActive(true);
		particlesSystem.Play();
		StartCoroutine(SetInactiveAfter(1));
	}
	private void Update()
	{
	}
}
