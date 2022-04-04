using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public float speed;
	public VariableJoystick variableJoystick;
	private Rigidbody rb;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		Vector3 direction = Vector3.forward * -variableJoystick.Vertical + Vector3.right * -variableJoystick.Horizontal;
		rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
		if (!direction.Equals(Vector3.zero))
		{
			rb.rotation = Quaternion.LookRotation(-direction * speed * Time.fixedDeltaTime);
		}
	}
}
