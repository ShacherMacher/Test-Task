using System.Collections;
using UnityEngine;

public class UAreaScript : MonoBehaviour
{
	public FactoryScript factoryScript;
	GameObject cargoObject;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
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
		if (cargoContr.stackObjects.Count > 0 && factoryScript.unloadArea.Count < 36 && cargoContr.GetTag() == "R1")
		{
			yield return new WaitForSecondsRealtime(.5f);
			cargoObject = cargoContr.OutItem();
			cargoObject.transform.SetParent(this.transform, false);
			int column = factoryScript.unloadArea.Count / 6;
			cargoObject.transform.localPosition = Vector3.Lerp(cargoObject.transform.position , new Vector3(1 - 0.4f * (factoryScript.unloadArea.Count - 6 * column), 0.2f, -1 + (0.4f * column)), 1);
			factoryScript.unloadArea.Push(cargoObject);
		}
		else yield break;
	}
}
