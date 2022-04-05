using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryScript : MonoBehaviour
{
	public GameObject resourceObject;
	public GameObject areaObject;
	public Transform uAreaObject;
	public Stack<GameObject> unloadArea = new Stack<GameObject>();
	public Stack<GameObject> productionArea = new Stack<GameObject>();
	public Stack<GameObject> unloadAreaSecond = new Stack<GameObject>();
	public int factoryNum;
	public Text textMsg;
	public float delayFactory;
	void Start()
	{
		switch (factoryNum)
		{
			case 1:
				{
					StartCoroutine(ProduceResource(delayFactory));
					break;
				}
			case 2:
				{
					StartCoroutine(ProduceResourceSecond(delayFactory));
					break;
				}
			case 3:
				{
					StartCoroutine(ProduceResourceThird(delayFactory));
					break;
				}
			default:
				{
					Debug.LogError("Enter Factory Number!");
					break;
				}
		}
	}

	public IEnumerator ProduceResource(float delay)
	{
		while (true)
		{
			if (productionArea.Count < 90)
			{
				ClearTxt();
				int column = productionArea.Count / 15;
				GameObject newObject = Instantiate(resourceObject, this.transform.position, Quaternion.identity);
				newObject.transform.SetParent(areaObject.transform, true);
				Vector3 targetPos = new Vector3(1.2f - (0.4f * column), 0.2f, 2.6f - 0.4f * (productionArea.Count - 15 * column));
				StartCoroutine(Move(targetPos, newObject));
				productionArea.Push(newObject);
				yield return new WaitForSecondsRealtime(delay);
			}
			else
			{
				ShowTxt("Factory 1 stopped: Full warehouse!");
				yield return null;
			}
		}
	}

	public IEnumerator ProduceResourceSecond(float delay)
	{
		while (true)
		{
			if (productionArea.Count < 36 && unloadArea.Count > 0)
			{
				ClearTxt();

				int column = productionArea.Count / 6;
				GameObject newObject = Instantiate(resourceObject, this.transform.position, Quaternion.identity);
				newObject.transform.SetParent(uAreaObject.transform, true);
				Vector3 targetPos = new Vector3(1 - 0.4f * (productionArea.Count - 6 * column), 0.2f, -1 + (0.4f * column));
				StartCoroutine(Move(targetPos, newObject));
				productionArea.Push(newObject);
				GameObject oldObject = unloadArea.Pop();
				oldObject.transform.SetParent(null);
				StartCoroutine(Move(gameObject.transform.position, oldObject));
				yield return new WaitForSecondsRealtime(delay);
				Destroy(oldObject);
			}
			else
			{
				if (productionArea.Count == 36) ShowTxt("Factory 2 stopped: Full warehouse!");
				if (unloadArea.Count == 0) ShowTxt("Factory 2 stopped: No resources!");
				yield return null;
			}
		}
	}

	IEnumerator Move(Vector3 targetPos, GameObject newObject)
	{
		float time = 0, duration = .5f;
		Vector3 startPosition = newObject.transform.localPosition;
		while (time < duration)
		{
			newObject.transform.localPosition = Vector3.Lerp(startPosition, targetPos, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		newObject.transform.localPosition = targetPos;
	}

	public IEnumerator ProduceResourceThird(float delay)
	{
		while (true)
		{
			if (productionArea.Count < 36 && unloadArea.Count > 0 && unloadAreaSecond.Count > 0)
			{
				ClearTxt();
				int column = productionArea.Count / 6;
				GameObject newObject = Instantiate(resourceObject, this.transform.position, Quaternion.identity);
				newObject.transform.SetParent(uAreaObject.transform, true);
				Vector3 targetPos = new Vector3(1 - 0.4f * (productionArea.Count - 6 * column), 0.2f, -1 + (0.4f * column));
				StartCoroutine(Move(targetPos, newObject));
				productionArea.Push(newObject);
				GameObject oldObject = unloadArea.Pop();
				GameObject oldObject2 = unloadAreaSecond.Pop();
				oldObject.transform.SetParent(null);
				oldObject2.transform.SetParent(null);
				StartCoroutine(Move(gameObject.transform.position, oldObject));
				StartCoroutine(Move(gameObject.transform.position, oldObject2));
				yield return new WaitForSecondsRealtime(delay);
				Destroy(oldObject);
				Destroy(oldObject2);
			}
			else
			{
				if (productionArea.Count == 36) ShowTxt("Factory 3 stopped: Full warehouse!");
				if (unloadArea.Count == 0 || unloadAreaSecond.Count == 0) ShowTxt("Factory 3 stopped: No resources!");
				yield return null;
			}
		}
	}

	void ShowTxt(string msg)
	{
		textMsg.text = msg;
	}

	void ClearTxt()
	{
		textMsg.text = null;
	}
}