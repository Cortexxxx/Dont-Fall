using UnityEngine;

public class CoinPool : Pool
{
	[SerializeField] private Coin coin;

	private PoolMono<Coin> pool;
	private void Start()
	{
		base.Start();
		pool = new PoolMono<Coin>(coin, poolCount, transform);
		pool.autoExpant = autoExpand;
	}

	protected override void Update()
	{
		base.Update();
		if (lastSpawnTime + cooldowns[(int)LevelManager.Instance.difficulty] < Time.time && isActive && isStarted)
		{
			lastSpawnTime = Time.time;
			CreateCoin();
		}

	}
	private void CreateCoin()
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
			Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
			Collider[] colliders = Physics.OverlapBox(hit.collider.gameObject.transform.position, new Vector3(0.5f, 0.5f, 0.5f));
			foreach (var item in colliders)
			{
				if (LayerMask.LayerToName(item.gameObject.layer) == "NoCoins")
				{
					continue;
				}
			}
			break;
		}

		var coin = pool.GetFreeElement();
		coin.transform.position = spawnPosition;
	}
}
