using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
	public T prefab { get; }
	private T[] prefabs { get; }
	public bool autoExpant { get; set; }
	public Transform container { get; set; }
	private List<T> pool;
	public PoolMono(T prefab, int count, Transform transform)
	{
		this.prefab = prefab;
		container = transform;
		CreatePool(count);
	}
	public PoolMono(T[] prefabs, int count, Transform transform)
	{
		this.prefabs = prefabs;
		container = transform;
		CreatePool(count);
	}

	private void CreatePool(int count)
	{
		pool = new List<T>();

		for (int i = 0; i < count; i++)
		{
			CreateObject();
		}
	}
	private T CreateObject(bool isActivebyDefault = false)
	{
		
		if (prefabs != null)
		{
			var createdObject = GameObject.Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length)], container);
			createdObject.gameObject.SetActive(isActivebyDefault);
			pool.Add(createdObject);
			return createdObject;
		}
		else
		{
			var createdObject = GameObject.Instantiate(prefab, container);
			createdObject.gameObject.SetActive(isActivebyDefault);
			pool.Add(createdObject);
			return createdObject;
		}

	}

	public bool HasFreeElement(out T element)
	{
		foreach (var mono in pool)
		{
			if (!mono.gameObject.activeInHierarchy)
			{
				element = mono;
				mono.gameObject.SetActive(true);
				return true;

			}
		}
		element = null;
		return false;
	}

	public T GetFreeElement()
	{
		if (HasFreeElement(out var element))
		{
			return element;
		}
		else if (autoExpant)
		{
			return CreateObject(true);
		}
		throw new Exception($"No free elements in the pool of type {typeof(T)}");
	}

	public T[] GetAllElements()
	{
		return pool.ToArray();
	}

}
