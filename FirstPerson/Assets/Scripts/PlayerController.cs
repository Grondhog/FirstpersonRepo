using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;
	
	[SerializeField]
	private float mouseSensitivity = 3f;
	
	private PlayerMotor motor;
	
	void Start()
	{
		motor = GetComponent<PlayerMotor>();
	}
	
	void Update()
	{
		//calculate velocity as 3D Vector
		float xMov = Input.GetAxisRaw("Horizontal");
		float zMov = Input.GetAxisRaw("Vertical");
		
		Vector3 movHorizontal = transform.right *xMov;
		Vector3 movVertical = transform.forward * zMov;
		
		//final movement vector
		Vector3 velocity = (movHorizontal + movVertical).normalized * speed;
		
		//apply this movement
		motor.Move(velocity);
		
		//rotation for turning only
		float yRot = Input.GetAxisRaw("Mouse X");
		Vector3 rotation = new Vector3(0f, yRot, 0f) * mouseSensitivity;
		
		motor.Rotate(rotation);
		
		float xRot = Input.GetAxisRaw("Mouse Y");
		
		Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * mouseSensitivity;
		
		motor.RotateCamera(cameraRotation);
		
	}
	
}
