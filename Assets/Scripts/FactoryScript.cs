using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryScript : MonoBehaviour
{
	public GameObject resourceObject;
	public GameObject areaObject;
	public Transform uAreaObject;
	public Stack<GameObject> unloadArea = new Stack<GameObject>();
	public Stack<GameObject> productionArea = new Stack<GameObject>();
	public Stack<GameObject> unloadAreaSecond = new Stack<GameObject>();
	public int factoryNum;
	void Start()
	{
		switch (factoryNum)
		{
			case 1:
				{
					StartCoroutine(ProduceResource());
					break;
				}
			case 2:
				{
					StartCoroutine(ProduceResourceSecond());
					break;
				}
			case 3:
				{
					StartCoroutine(ProduceResourceThird());
					break;
				}
			default:
				{
					Debug.LogError("Enter Factory Number!");
					break;
				}
		}
	}

	public IEnumerator ProduceResource()
	{
		while (true)
		{
			if (productionArea.Count < 90)
			{
				int column = productionArea.Count / 15;
				GameObject newObject = Instantiate(resourceObject);
				newObject.transform.SetParent(areaObject.transform, true);
				newObject.transform.localPosition = new Vector3(1.2f - (0.4f * column), 0.2f, 2.6f - 0.4f * (productionArea.Count - 15 * column));
				productionArea.Push(newObject);
				yield return new WaitForSecondsRealtime(.1f);
			}
			else yield return null;
		}
	}

	public IEnumerator ProduceResourceSecond()
	{
		while (true)
		{
			if (productionArea.Count < 36 && unloadArea.Count > 0)
			{
				int column = productionArea.Count / 6;
				GameObject newObject = Instantiate(resourceObject);
				newObject.transform.SetParent(uAreaObject.transform, true);
				newObject.transform.localPosition = new Vector3(1 - 0.4f * (productionArea.Count - 6 * column), 0.2f, -1 + (0.4f * column));
				productionArea.Push(newObject);
				Destroy(unloadArea.Pop());
				yield return new WaitForSecondsRealtime(.1f);
			}
			else yield return null;
		}
	}

	public IEnumerator ProduceResourceThird()
	{
		while (true)
		{
			if (productionArea.Count < 36 && unloadArea.Count > 0 && unloadAreaSecond.Count > 0)
			{
				int column = productionArea.Count / 6;
				GameObject newObject = Instantiate(resourceObject);
				newObject.transform.SetParent(uAreaObject.transform, true);
				newObject.transform.localPosition = new Vector3(1 - 0.4f * (productionArea.Count - 6 * column), 0.2f, -1 + (0.4f * column));
				productionArea.Push(newObject);
				Destroy(unloadArea.Pop());
				Destroy(unloadAreaSecond.Pop());
				yield return new WaitForSecondsRealtime(.1f);
			}
			else yield return null;
		}
	}
}