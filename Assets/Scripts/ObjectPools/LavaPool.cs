using UnityEngine;

public class LavaPool : Pool
{
	[SerializeField] private Lava lava;

	private PoolMono<Lava> pool;
	protected override void Start()
	{
		base.Start();
		pool = new PoolMono<Lava>(lava, poolCount, transform);
		pool.autoExpant = autoExpand;
	}

	protected override void Update()
	{
		base.Update();

		if (!isActive)
		{
			var lavas = pool.GetAllElements();
			foreach (var lava in lavas)
			{
				if (lava.gameObject.activeSelf)
				{
					lava.Crash();
				}
			}
			enabled = false;
		}
	}

	protected override void CreateObject()
	{
		// sideLength = Width of the scene devided 2. It is the area where objects can spawn
		var lava = pool.GetFreeElement();

		Rigidbody rb = lava.GetComponent<Rigidbody>();
		int side = Random.Range(1, 5);
		Vector3 spawnPosition = new Vector3();
		Vector3 forceDirection = new Vector3(lava.GetComponent<Lava>().force, lava.GetComponent<Lava>().force * 2, lava.GetComponent<Lava>().force);
		Vector3 platformSize = LevelManager.Instance.platformSizes[(int)LevelManager.Difficulties.Medium];
		switch (side)
		{
			case 1:
				spawnPosition = new Vector3(platformSize.x + 5, -5, Random.Range(-platformSize.x / 2, platformSize.x / 2)); // +x
				forceDirection = new Vector3(-forceDirection.x, forceDirection.y, 0);
				rb.GetComponent<Animator>().SetFloat("Angle", 0);
				break;
			case 2:
				spawnPosition = new Vector3(-platformSize.x - 5, -5, Random.Range(-platformSize.x / 2, platformSize.x / 2));// -x
				forceDirection = new Vector3(forceDirection.x, forceDirection.y, 0);
				rb.GetComponent<Animator>().SetFloat("Angle", 180);

				break;
			case 3:
				spawnPosition = new Vector3(Random.Range(-platformSize.x / 2, platformSize.x / 2), -5, platformSize.x + 5); // +z
				forceDirection = new Vector3(0, forceDirection.y, -forceDirection.z);
				rb.GetComponent<Animator>().SetFloat("Angle", 90);

				break;
			case 4:
				spawnPosition = new Vector3(Random.Range(-platformSize.x / 2, platformSize.x / 2), -5, -platformSize.x - 5); // -z
				forceDirection = new Vector3(0, forceDirection.y, forceDirection.z);
				rb.GetComponent<Animator>().SetFloat("Angle", 270);

				break;
			case 5:
				Debug.Log("SOSI");
				break;
			default:
				break;
		}

		rb.transform.position = spawnPosition;

		rb.AddForce(forceDirection * rb.mass, ForceMode.VelocityChange);
	}
}
