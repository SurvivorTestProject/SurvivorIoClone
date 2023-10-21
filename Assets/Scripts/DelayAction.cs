using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAction : MonoBehaviour
{
	private void Delay(List<DelayItem> delayItems)
	{
		StartCoroutine(generateEnumerator(delayItems));
	}

	private IEnumerator generateEnumerator(List<DelayItem> delayItems)
	{
		foreach (DelayItem delayItem in delayItems)
		{
			yield return new WaitForSeconds(delayItem.timeDelay);
			delayItem.function();
		}
	}
}
