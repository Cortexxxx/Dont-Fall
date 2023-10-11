using UnityEngine;

public class CoinPool : Pool
{
	[SerializeField] private Coin coin;

	private PoolMono<Coin> pool;
	private void Start()
	{
		pool = new PoolMono<Coin>(coin, poolCount, transform);
		pool.autoExpant = autoExpand;
	}

	protected override void Update()
	{
		base.Update();
		if (lastSpawnTime + cooldowns[(int)LevelManager.Instance.difficulty] < Time.time && isActive)
		{
			lastSpawnTime = Time.time;
			CreateCoin();
		}

	}
	private void CreateCoin()
	{
		// sideLength = Width of the scene devided 2. It is the area where objects can spawn
		float sideLength = LevelManager.Instance.platformSizes[(int)LevelManager.Instance.difficulty].x / 2;
		Vector3 spawnPosition = new Vector3(Random.Range(-sideLength, sideLength), 15, Random.Range(-sideLength, sideLength));
		var coin = pool.GetFreeElement();
		coin.transform.position = spawnPosition;
	}
}
