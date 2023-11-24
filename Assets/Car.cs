using UnityEngine;
using System;

public class Car : PoolObject
{
	[SerializeField] private float speed = 10;
	bool isDriving = false;
	private void OnEnable()
	{
		isDriving = true;
	}
	private void OnDisable()
	{
		isDriving = false;
	}
	private void Update()
	{
		if (isDriving)
		{
			transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
		}
	}
}
