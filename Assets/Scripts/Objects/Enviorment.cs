using UnityEngine;

public class Enviorment : MonoBehaviour
{
	[SerializeField] private GameObject easyEnviorment;
	[SerializeField] private GameObject mediumEnviorment;
	[SerializeField] private GameObject hardEnviorment;
	[SerializeField] private Transform ground;
	[SerializeField] private bool hasGround = true;
	private void Start()
	{
		if (hasGround)
		{
			switch (LevelManager.Instance.difficulty)
			{
				case LevelManager.Difficulties.Easy:
					ground.localScale = LevelManager.Instance.platformSizes[0];
					easyEnviorment.SetActive(true);
					break;
				case LevelManager.Difficulties.Medium:
					ground.localScale = LevelManager.Instance.platformSizes[1];
					mediumEnviorment.SetActive(true);
					break;
				case LevelManager.Difficulties.Hard:
					ground.localScale = LevelManager.Instance.platformSizes[2];
					hardEnviorment.SetActive(true);
					break;
				default:
					break;
			}
		}

	}
}
