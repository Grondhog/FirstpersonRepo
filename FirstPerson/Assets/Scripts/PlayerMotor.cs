using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera cam;
	
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private Vector3 cameraRotation = Vector3.zero;

    private Vector3 jumpSpeed = Vector3.zero;
    private Vector3 gravity = new Vector3(0, -1, 0);


    private Rigidbody rb;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}
	
	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}
	
	public void RotateCamera(Vector3 _cameraRotation)
	{
		cameraRotation = _cameraRotation;
	}

    public void ChangeGravity(bool isGravityOn)
    {
        rb.useGravity = isGravityOn;
    }

    public void ApplyJump(Vector3 _jumpSpeed)
    {
        jumpSpeed = _jumpSpeed;
    }

    void FixedUpdate()
	{
		
		PerformMovement();
		PerformRotation();
        //AddGravity();
	}

    private void AddGravity()
    {
        rb.AddForce(transform.position + gravity * Time.fixedDeltaTime);

    }

    private void PerformRotation()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if(cam != null)
		{
			cam.transform.Rotate(-cameraRotation);
		}
	}

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition((transform.position + velocity * Time.fixedDeltaTime));
        }
        if (jumpSpeed != Vector3.zero)
        {
            rb.AddForce(jumpSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
    }
}
