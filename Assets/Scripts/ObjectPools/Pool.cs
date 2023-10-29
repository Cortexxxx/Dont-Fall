using System.Collections;
using UnityEngine;

public class Pool : MonoBehaviour
{
	[SerializeField] protected int poolCount = 10;
	[SerializeField] protected int delay = 5;
	[SerializeField] protected bool autoExpand = false;
	[SerializeField] protected float[] cooldowns;
	[SerializeField] protected float lastSpawnTime;
	protected bool isActive = true;
	protected bool isStarted = false;
	protected virtual void Start()
	{
		StartCoroutine(WaitForStart());
	}

	protected virtual void Update()
	{
		if (LevelManager.Instance.isCompleted == true)
		{
			isActive = false;
		}
	}
	private IEnumerator WaitForStart()
	{
		yield return new WaitForSeconds(delay);
		isStarted = true;

	}
}
