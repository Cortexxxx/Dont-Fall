using UnityEngine;

public class Pool : MonoBehaviour
{
	[SerializeField] protected int poolCount = 10;
	[SerializeField] protected bool autoExpand = false;
	[SerializeField] protected float[] cooldowns;
	protected float lastSpawnTime;
	protected bool isActive = true;

	private void Start()
	{
		isActive = true;
	}

	protected virtual void Update()
	{
		if (LevelManager.Instance.isCompleted == true)
		{
			isActive = false;
		}
	}

}
