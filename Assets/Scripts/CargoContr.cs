using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoContr : MonoBehaviour
{
	public Transform CargoPos;
	public Stack<GameObject> stackObjects = new Stack<GameObject>();

	public void AddItem(GameObject cargo)
	{
		stackObjects.Push(cargo);
		Debug.Log(stackObjects.Count);
		cargo.transform.SetParent(CargoPos, true);
		StartCoroutine(Move(cargo));
		cargo.transform.localRotation = Quaternion.identity;
	}

	private IEnumerator Move(GameObject cargo)
	{
		float desiredDuration = .01f;
		for (float t = 0; t < 1; t += Time.deltaTime / desiredDuration)
		{
			cargo.transform.localPosition = Vector3.LerpUnclamped(cargo.transform.position, new Vector3(0, .35f * stackObjects.Count, 0), t);
			yield return new WaitForFixedUpdate();
		}
		yield return null;
	}

	public GameObject OutItem()
	{
	 	return stackObjects.Pop();
	}

	public string GetTag()
	{
		return stackObjects.Peek().tag;
	}
}
