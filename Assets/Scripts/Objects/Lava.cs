using System.Collections;
using UnityEngine;

public class Lava : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField] private GameObject prefab;
	[SerializeField] private ParticleSystem particlesSystem;
	[SerializeField] private int defaultSize;
	public int force;
	private bool isChild = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void OnEnable()
	{
		GetComponent<Animator>().enabled = true;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.GetComponent<Player>())
		{
			Player.Instance.Die();
			rb.constraints = RigidbodyConstraints.FreezeAll;
			Crash();
		}
		if (transform.localScale.x == defaultSize)
		{
			isChild = true;
			for (int i = 0; i < 8; i++)
			{
				GameObject lavaSmall = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
				lavaSmall.transform.localScale = prefab.transform.localScale / 2;
				Rigidbody rbsmall = lavaSmall.GetComponent<Rigidbody>();
				Vector3 f = new Vector3(5 + Random.Range(-1f,1f), 5, 5 + Random.Range(-1f, 1f));
				switch (i)
				{
					case 0:
						rbsmall.AddForce(new Vector3(f.x, f.y, 0), ForceMode.Impulse);
						break;
					case 1:
						rbsmall.AddForce(new Vector3(-f.x, f.y, 0), ForceMode.Impulse);
						break;
					case 2:
						rbsmall.AddForce(new Vector3(0, f.y, f.z), ForceMode.Impulse);
						break;
					case 3:
						rbsmall.AddForce(new Vector3(0, f.y, -f.z), ForceMode.Impulse);
						break;
					case 4:
						rbsmall.AddForce(new Vector3(f.x / 2, f.y, f.x / 2), ForceMode.Impulse);
						break;
					case 5:
						rbsmall.AddForce(new Vector3(-f.x / 2, f.y, f.x / 2), ForceMode.Impulse);
						break;
					case 6:
						rbsmall.AddForce(new Vector3(f.x / 2, f.y, -f.z / 2), ForceMode.Impulse);
						break;
					case 7:
						rbsmall.AddForce(new Vector3(-f.x / 2, f.y, -f.z / 2), ForceMode.Impulse);
						break;
				}
			}
			Crash();
		}
		else
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
