using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
	public static Player Instance;
	[SerializeField] private ParticleSystem ParticleSystem;

	private void Awake()
	{
		// Singleton
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		transform.position = new Vector3(0,1,0);
	}
	private void Start()
	{
		Coins = PlayerPrefs.GetInt("Coins");
	}
	private void Update()
	{
		if (transform.position.y < -5)
		{
			Die();
		}
	}

	private IEnumerator FadeDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene("menu");
	}

	private int coins = 0;
	public int Coins
	{
		get { return coins; }
		set
		{

			PlayerPrefs.SetInt("Coins", value);
			UIContainer.Instance.coinText.GetComponent<TextMeshProUGUI>().text = value.ToString();
			coins = value;
		}
	}
	public void Die()
	{
		GetComponent<PlayerMovement>().enabled = false;
		GetComponent<CharacterController>().enabled = false;
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		ParticleSystem.gameObject.SetActive(true);
		ParticleSystem.Play();
		Destroy(gameObject, 1);
		Animator fadeAnimator = Fade.Instance.fade.GetComponent<Animator>();
		fadeAnimator.SetBool("Activate", true);
		float delay = fadeAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
		StartCoroutine(FadeDelay(delay));
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponentInParent<Portal>())
		{
			GetComponent<PlayerMovement>().enabled = false;
			GetComponent<Animator>().SetBool("isWalking", false);
			other.GetComponentInParent<Portal>().FinishLevel();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.GetComponent<Coin>())
		{
			collision.collider.GetComponent<Coin>().Collect();
		}
	}
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.GetComponent<Coin>())
		{
			hit.collider.GetComponent<Coin>().Collect();
		}
	}

	public static int GetLevel(int xp)
	{
		int level = 0;
		int tempxp = 0;
		int counter = 0;
		while (tempxp <= xp)
		{
			tempxp += 50 * counter;
			counter++;
			level++;
		}
		return level - 1;
	}
	public static int GetLevel()
	{
		int xp = PlayerPrefs.GetInt("xp");
		int level = 0;
		int tempxp = 0;
		int counter = 0;
		while (tempxp <= xp)
		{
			tempxp += 50 * counter;
			counter++;
			level++;
		}
		return level - 1;
	}
}
static class Extention
{
	public static K FindFirstKeyByValue<K, V>(this Dictionary<K, V> dict, V val)
	{
		return dict.FirstOrDefault(entry =>
			EqualityComparer<V>.Default.Equals(entry.Value, val)).Key;
	}
}
