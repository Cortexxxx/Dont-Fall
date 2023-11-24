using System.Collections.Generic;
using UnityEngine;

public class Road : Pool
{
	[SerializeField] private Car[] cars;
	[SerializeField] private Transform[] xPositiveSpawns;
	[SerializeField] private Transform[] xNegativeSpawns;
	[SerializeField] private Transform[] zPositiveSpawns;
	[SerializeField] private Transform[] zNegativeSpawns;
	private PoolMono<Car> pool;
	protected override void Start()
	{
		base.Start();
		pool = new PoolMono<Car>(cars, poolCount, transform);
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
		int sideToSpawn = Random.Range(0, 4);
		int roadToSpawn = Random.Range(0, 3 - (int)LevelManager.Instance.difficulty);
		int rotation = sideToSpawn * 90;
		if (rotation == 90) {
			rotation = 270;
		} else if (rotation == 270) { 
			rotation = 90;
		}
		Car car = pool.GetFreeElement();	
		Transform[] currSpawns = null;
		Debug.Log(sideToSpawn);
		switch (sideToSpawn)
		{
			case 0:
				currSpawns = zNegativeSpawns;
				break;
			case 1:
				currSpawns = xPositiveSpawns;
				break;
			case 2:
				currSpawns = zPositiveSpawns;
				break;
			case 3:
				currSpawns = xNegativeSpawns;
				break;
			default:
				break;
		}
		car.gameObject.transform.position = currSpawns[roadToSpawn].position;
		car.gameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
	}

}
