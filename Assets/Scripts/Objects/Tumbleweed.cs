using System.Collections;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField] private ParticleSystem particlesSystem;
	[SerializeField] private int collideWithTumbleweedLayer = 9;
	public int[] forces;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void OnEnable()
	{
		GetComponent<Animator>().enabled = true;
	}
	private void OnTriggerEnter(Collider other)
	{
		rb.constraints = RigidbodyConstraints.None;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.GetComponent<Player>())
		{
			Player.Instance.Die();
			rb.constraints = RigidbodyConstraints.FreezeAll;
			Crash();
		} else if (collision.collider.gameObject.layer == collideWithTumbleweedLayer && collision.collider.gameObject.tag != "Ground")
		{
			Crash();
			rb.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
	private IEnumerator SetInactiveAfter(float time)
	{
		yield return new WaitForSeconds(time);
		GetComponentInChildren<MeshRenderer>().enabled = true;
		GetComponent<Collider>().enabled = true;
		particlesSystem.Stop();
		particlesSystem.gameObject.SetActive(false);
		rb.constraints = RigidbodyConstraints.None;
		GetComponent<Animator>().enabled = true;
		gameObject.SetActive(false);

	}

	public void Crash()
	{
		rb.constraints = RigidbodyConstraints.FreezeAll;
		GetComponent<Animator>().enabled = false;
		GetComponentInChildren<MeshRenderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		particlesSystem.gameObject.SetActive(true);
		particlesSystem.Play();
		StartCoroutine(SetInactiveAfter(1));
	}
}
