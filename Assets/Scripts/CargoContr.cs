using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoContr : MonoBehaviour
{
	public Transform CargoPos;
	public Stack<GameObject> stackObjects = new Stack<GameObject>();
	public int capacity;

	public void AddItem(GameObject cargo)
	{
		stackObjects.Push(cargo);
		cargo.transform.SetParent(CargoPos, true);
		StartCoroutine(Move(cargo, .35f * stackObjects.Count));
		cargo.transform.localRotation = Quaternion.identity;
	}

	private IEnumerator Move(GameObject cargo, float pos)
	{
		float elapsedTime = 0, waitTime = 1f;
		while (elapsedTime < waitTime)
		{
			cargo.transform.position = Vector3.Lerp(cargo.transform.position, new Vector3(CargoPos.position.x, CargoPos.position.y + pos, CargoPos.position.z), (elapsedTime / waitTime));
			elapsedTime += Time.deltaTime / 2;
			yield return null;
		}
		cargo.transform.position = new Vector3(CargoPos.position.x, CargoPos.position.y + pos, CargoPos.position.z);
		yield return null;
	}

	public GameObject OutItem()
	{
		if (stackObjects.Count > 0) return stackObjects.Pop();
		else return null;
	}

	public string GetTag()
	{
		return stackObjects.Peek().tag;
	}
}
