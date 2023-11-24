using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
	public ParticleSystem particles;
	public AudioClip[] clips;
	public GameObject meshparent;
	[HideInInspector] public bool hasAnimator = false;
	[HideInInspector] public bool hasParticles = false;
	[HideInInspector] public bool hasAudio = false;
	protected virtual void Start()
	{
		if (clips.Length > 0)
		{
			hasAudio = true;
		}
		else
		{
			hasAudio = false;
		}
		if (GetComponent<Animator>())
		{
			hasAnimator = true;
		}
		else
		{
			hasAnimator = false;
		}
		if (particles)
		{
			hasParticles = true;
		}
		else
		{
			hasParticles = false;
		}

	}

	private IEnumerator SetInactiveAfter(float time)
	{
		yield return new WaitForSeconds(time);
		if (hasAnimator)
		{
			GetComponent<Animator>().enabled = true;
		}
		if (hasParticles)
		{
			particles.Stop();
			particles.gameObject.SetActive(false);
		}
		if (GetComponent<MeshRenderer>())
		{
			GetComponent<MeshRenderer>().enabled = true;
		}
		else
		{
			meshparent.SetActive(true);
		}
		GetComponent<Collider>().enabled = true;
		gameObject.SetActive(false);
	}
	public void Crash()
	{
		if (hasAnimator)
		{
			GetComponent<Animator>().enabled = false;
		}
		if (hasParticles)
		{
			particles.gameObject.SetActive(true);
			particles.Play();
		}
		if (hasAudio)
		{
			GetComponent<AudioSource>().clip = clips[Random.Range(0, clips.Length)];
			GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
			GetComponent<AudioSource>().Play();
		}
		transform.rotation = Quaternion.identity;
		if (GetComponent<MeshRenderer>())
		{
			GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			meshparent.SetActive(false);
		}
		GetComponent<Collider>().enabled = false;
		StartCoroutine(SetInactiveAfter(1));
	}
	protected virtual void OnCollisionEnter(Collision collision)
	{
		Player player = collision.collider.GetComponent<Player>();
		if (player)
		{
			Player.Instance.Die();
			Crash();
		}
	}
}
