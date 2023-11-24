using UnityEngine;

public class Rock : PoolObject
{
	[SerializeField] private int[] fallForces;
	private void OnEnable()
	{
		GetComponent<Rigidbody>().AddForce(0,-1000 * fallForces[(int)LevelManager.Instance.difficulty] * Time.deltaTime, 0);
	}
	protected override void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter(collision);
		Crash();
	}
}
