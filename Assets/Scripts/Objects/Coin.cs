using UnityEngine;

public class Coin : PoolObject
{
	[SerializeField] private int[] coinAmmounts;
	[SerializeField] private int[] fallForces;

	private void OnEnable()
	{
		GetComponent<Rigidbody>().AddForce(0, -100 * fallForces[(int)LevelManager.Instance.difficulty], 0);
	}

	public void Collect()
	{
			Player.Instance.Coins += coinAmmounts[(int)LevelManager.Instance.difficulty] - 1 + Random.Range(0, 1) * ((int)LevelManager.Instance.difficulty + 1);
			Crash();
	}
	protected override void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.GetComponent<Player>())
		{
			Debug.Log("BEB");
			Collect();
		}
	}
}
