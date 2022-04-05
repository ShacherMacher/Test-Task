using System.Collections;
using UnityEngine;

public class U2AreaScript : MonoBehaviour
{
	public FactoryScript factoryScript;
	GameObject cargoObject;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			CargoContr cargoContr;
			if (other.TryGetComponent(out cargoContr) && cargoContr.stackObjects.Count > 0 && factoryScript.unloadAreaSecond.Count < 36 && cargoContr.GetTag() == "R2")
			{
				StartCoroutine(Pause());
				cargoObject = cargoContr.OutItem();
				if (cargoObject != null)
				{
					cargoObject.transform.SetParent(this.transform, false);
					int column = factoryScript.unloadAreaSecond.Count / 6;
					cargoObject.transform.localPosition = Vector3.Lerp(cargoObject.transform.position, new Vector3(1 - 0.4f * (factoryScript.unloadAreaSecond.Count - 6 * column), 0.2f, -1 + (0.4f * column)), 1);
					factoryScript.unloadAreaSecond.Push(cargoObject);
				}
			}
		}
	}

	public IEnumerator Pause()
	{
		yield return new WaitForSecondsRealtime(.5f);

	}
}
