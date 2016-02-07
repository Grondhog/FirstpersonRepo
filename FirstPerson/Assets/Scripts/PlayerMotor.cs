using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera cam;
	
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
    private Vector3 jump = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    private bool onWall;

	UnityStandardAssets.Utility.LerpControlledBob bobber;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		bobber = GetComponent<UnityStandardAssets.Utility.LerpControlledBob>();
	}
	
	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}
	
	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

    public void Jump(Vector3 _jump)
    {
        jump = _jump;
    }
	
	public void RotateCamera(float _cameraRotation)
	{
		cameraRotationX = _cameraRotation;
	}

    void FixedUpdate()
	{
		
		PerformMovement();
		PerformRotation();
       
	}

    private void PerformRotation()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if(cam != null)
		{
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
		}
	}



    private void PerformMovement()
    {
    	//print("Velocity: " + velocity + ", jump: " + jump); 
        if (velocity != Vector3.zero)// && !onWall)
        {
			//bobber.DoBobCycle();
			//velocity.y += bobber.Offset();
			RaycastHit hit;
			if(rb.SweepTest(velocity, out hit, velocity.magnitude * Time.fixedDeltaTime))
			{
				if(!hit.collider.tag.Equals("Exit"))
				{
					//print("Velocity: " + velocity + ", jump: " + jump); 
					velocity = Vector3.zero;
				}

			}

            rb.MovePosition((transform.position + velocity * Time.fixedDeltaTime));

            //print(velocity );//* Time.fixedDeltaTime);
            //rb.AddForce(velocity );
        }
        if(jump != Vector3.zero)
        {
            rb.AddForce(jump * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
}
