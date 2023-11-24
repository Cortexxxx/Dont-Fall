using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Tumbleweed : PoolObject
{
	private Rigidbody rb;
	[SerializeField] private int collideWithTumbleweedLayer = 9;
	public int[] forces;

	protected override void Start()
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
	protected override void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter(collision);
		if (collision.collider.GetComponent<Player>())
		{
			rb.constraints = RigidbodyConstraints.FreezeAll;
		} else if (collision.collider.gameObject.layer == collideWithTumbleweedLayer && collision.collider.gameObject.tag != "Ground")
		{
			Crash();
			rb.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

}
