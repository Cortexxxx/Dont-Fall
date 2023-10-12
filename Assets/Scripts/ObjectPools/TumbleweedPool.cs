using UnityEngine;

public class TumbleweedPool : Pool
{
	[SerializeField] private Tumbleweed tumbleweed;

	private PoolMono<Tumbleweed> pool;
	private void Start()
	{
		base.Start();
		pool = new PoolMono<Tumbleweed>(tumbleweed, poolCount, transform);
		pool.autoExpant = autoExpand;
	}

	protected override void Update()
	{
		base.Update();
		if (lastSpawnTime + cooldowns[(int)LevelManager.Instance.difficulty] < Time.time && isActive && isStarted)
		{
			lastSpawnTime = Time.time;
			CreateTumbleweed();
		}
		if (!isActive)
		{
			var tumbleweeds = pool.GetAllElements();
			foreach (var tumbleweed in tumbleweeds)
			{
				if (tumbleweed.gameObject.activeSelf)
				{
					tumbleweed.Crash();
				}
			}
			enabled = false;
		}
	}
	private void CreateTumbleweed()
	{
		// sideLength = Width of the scene devided 2. It is the area where objects can spawn
		var tumbleweed = pool.GetFreeElement();

		Rigidbody rb = tumbleweed.GetComponent<Rigidbody>();
		int side = Random.Range(1, 4);
		Vector3 spawnPosition = new Vector3();
		Vector3 forceDirection = new Vector3();
		Vector3 platformSize = LevelManager.Instance.platformSizes[(int)LevelManager.Difficulties.Medium];
		switch (side)
		{
			case 1:
				spawnPosition = new Vector3(platformSize.x,2, Random.Range(-platformSize.x / 2, platformSize.x / 2));
				forceDirection = new Vector3(-tumbleweed.GetComponent<Tumbleweed>().forces[(int)LevelManager.Difficulties.Medium], 0, 0);
				break;
			case 2:
				spawnPosition = new Vector3(-platformSize.x, 2, Random.Range(-platformSize.x / 2, platformSize.x / 2));
				forceDirection = new Vector3(tumbleweed.GetComponent<Tumbleweed>().forces[(int)LevelManager.Difficulties.Medium], 0, 0);
				break;
			case 3:
				spawnPosition = new Vector3(Random.Range(-platformSize.x / 2, platformSize.x / 2), 2, platformSize.x);
				forceDirection = new Vector3(0, 0,-tumbleweed.GetComponent<Tumbleweed>().forces[(int)LevelManager.Difficulties.Medium]);
				break;
			case 4:
				spawnPosition = new Vector3(Random.Range(-platformSize.x / 2, platformSize.x / 2), 2 , -platformSize.x);
				forceDirection = new Vector3(0, 0, tumbleweed.GetComponent<Tumbleweed>().forces[(int)LevelManager.Difficulties.Medium]);
				break;
			default:
				break;
		}

		rb.transform.position = spawnPosition;

		rb.AddForce(forceDirection, ForceMode.Impulse);
		rb.constraints = RigidbodyConstraints.FreezePositionY;
	}
}
