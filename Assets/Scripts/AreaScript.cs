using System.Collections;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
	public FactoryScript factoryScript;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && factoryScript.productionArea.Count != 0)
		{
			CargoContr cargoContr;
			if (other.TryGetComponent(out cargoContr))
			{
				if (cargoContr.stackObjects.Count <= cargoContr.capacity)
				{
					cargoContr.AddItem(factoryScript.productionArea.Pop());
				}
				StartCoroutine(Pause());
			}
		}
	}

	public IEnumerator Pause()
	{
		yield return new WaitForSeconds(1f);
	}
}
