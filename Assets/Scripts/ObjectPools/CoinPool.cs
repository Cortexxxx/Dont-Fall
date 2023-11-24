using UnityEngine;

public class CoinPool : Pool
{
	[SerializeField] private Coin coin;
	private PoolMono<Coin> pool;
	protected override void Start()
	{
		base.Start();
		pool = new PoolMono<Coin>(coin, poolCount, transform);
		pool.autoExpant = autoExpand;
	}
	protected override void CreateObject()
	{
		// sideLength = Width of the scene devided 2. It is the area where objects can spawn
		float sideLength = LevelManager.Instance.platformSizes[(int)LevelManager.Instance.difficulty].x / 2;
		Vector3 spawnPosition;
		while (true)
		{
			spawnPosition = new Vector3(Random.Range(-sideLength, sideLength), 15, Random.Range(-sideLength, sideLength));
			RaycastHit hit;
			Ray ray = new Ray(spawnPosition, Vector3.down * 20);
			Physics.Raycast(ray, out hit);
			if (hit.collider != null)
			{
				Collider[] colliders = Physics.OverlapBox(hit.collider.gameObject.transform.position, new Vector3(0.5f, 0.5f, 0.5f));
				foreach (var item in colliders)
				{
					if (LayerMask.LayerToName(item.gameObject.layer) == "NoCoins")
					{
						continue;
					}
				}
			}
			break;
		}
		var coin = pool.GetFreeElement();
		coin.transform.position = spawnPosition;
	}
}
