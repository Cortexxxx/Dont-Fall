using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : PoolObject
{
	[SerializeField] private int[] shootForces;
	private void OnEnable()
	{
		Vector3 direction = Player.Instance.transform.position - GetComponentInParent<Turret>().tower.transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		rotation.eulerAngles = new Vector3(0, 0, rotation.eulerAngles.z);
		Vector3 newDirection = Vector3.RotateTowards(GetComponentInParent<Turret>().tower.transform.forward, direction, 10 * Time.deltaTime, 0.0f);
		newDirection.y = 0;
		transform.position = GetComponentInParent<Turret>().snowballSpawnpoint.position;
		transform.parent = GetComponentInParent<Turret>().transform;
		GetComponent<Rigidbody>().AddForce(newDirection * 400);
	}


}
