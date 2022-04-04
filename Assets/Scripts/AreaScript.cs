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
				StartCoroutine(Load(cargoContr));
			}
		}
	}

	public IEnumerator Load(CargoContr cargoContr)
	{
		if (cargoContr.stackObjects.Count <= 10)
		{
			yield return new WaitForSecondsRealtime(.5f);
			cargoContr.AddItem(factoryScript.productionArea.Pop());
		}
		else yield break;
	}
}
