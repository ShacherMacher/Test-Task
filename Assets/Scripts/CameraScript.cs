using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Transform player;

	void Update()
	{
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 7, player.transform.position.z + 8);
	}
}
