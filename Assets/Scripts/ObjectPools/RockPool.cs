using UnityEngine;

public class RockPool : Pool
{
	[SerializeField] private Rock rock;

	private PoolMono<Rock> pool;
	protected override void Start()
	{
		base.Start();
		pool = new PoolMono<Rock>(rock, poolCount, transform);
		pool.autoExpant = autoExpand;
	}

	protected override void Update()
	{
		base.Update();

	
		if (!isActive)
		{

			var rocks = pool.GetAllElements();
			foreach (var rock in rocks)
			{
				if (rock.gameObject.activeSelf)
				{
					rock.Crash();
				}
			}
		}
	}

	protected override void CreateObject()
	{
		float sideLength = LevelManager.Instance.platformSizes[(int)LevelManager.Instance.difficulty].x / 2;
		Vector3 spawnPosition = new Vector3(Random.Range(-sideLength, sideLength), 15, Random.Range(-sideLength, sideLength));
		var rock = pool.GetFreeElement();
		rock.transform.position = spawnPosition;
	}
}
