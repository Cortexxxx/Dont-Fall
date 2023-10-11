using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	[SerializeField] private float time;
	[SerializeField] private TextMeshProUGUI text;
	[SerializeField] private Image fill;
	[SerializeField] private float max;
	[SerializeField] private Color yellowColor;
	[SerializeField] private Color redColor;
	[SerializeField] private Color greenColor;
	[SerializeField] private int[] times;
	private bool timeOut = false;

	private void Start()
	{
		max = times[(int)LevelManager.Instance.difficulty];
		time = max;
	}

	private void Update()
	{
		// Timer
		if (!timeOut)
		{
			time -= Time.deltaTime;
			text.text = "" + ((int)(time + 0.9f));
			fill.fillAmount = time / max;

			// The remaining time depends on the color of the timer 
			if (time / max > 0.6)
			{
				fill.color = greenColor;
				text.color = greenColor;
			} else if (time / max > 0.3)
			{
				fill.color = yellowColor;
				text.color = yellowColor;

			}
			else {
				fill.color = redColor;
				text.color = redColor;
			}

			if (time <= 0)
			{
				timeOut = true;
				LevelManager.Instance.isCompleted = true;
			}
		}

	}
}
