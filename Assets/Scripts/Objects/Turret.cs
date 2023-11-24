using System.Collections;
using System.Drawing;
using UnityEngine;

public class Turret : Pool
{
	public GameObject tower;
	public Transform snowballSpawnpoint;
	[SerializeField] private Snowball snowball;
	[SerializeField] private float[] delays;
	[SerializeField] private float startDelay = 5;
	private PoolMono<Snowball> pool;
	protected override void Start()
	{
		base.Start();
		pool = new PoolMono<Snowball>(snowball, poolCount, transform);
		pool.autoExpant = autoExpand;
		Rotate();
	}

	protected override void Update()
	{
		base.Update();
		if (Player.Instance && isActive && isStarted)
		{
			Rotate();
		}


		if (!isActive)
		{
			var snowballs = pool.GetAllElements();
			foreach (var snowball in snowballs)
			{
				if (snowball.gameObject.activeSelf)
				{
					snowball.Crash();
				}
			}
			enabled = false;
		}

	}
	protected override void CreateObject()
	{
		var snowball = pool.GetFreeElement();
	}
	private void Rotate()
	{
		Vector3 direction = Player.Instance.transform.position - tower.transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		rotation.eulerAngles = new Vector3(0, 0, rotation.eulerAngles.z);
		Vector3 newDirection = Vector3.RotateTowards(tower.transform.forward, direction, 10 * Time.deltaTime, 0.0f);
		newDirection.y = 0;
		tower.transform.rotation = Quaternion.LookRotation(newDirection);
	}
}
