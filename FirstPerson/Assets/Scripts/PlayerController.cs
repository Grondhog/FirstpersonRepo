using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Collider))]

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float mouseSensitivity = 3f;

    private PlayerMotor motor;

    [SerializeField]
    private float jumpForce = 1000f;
    
    private ConfigurableJoint joint;

    private Collider mCollider;
    [Header("Joint Settings")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    [SerializeField]
    private GameManager manager;

    private bool onFloor;

    void Start()
	{
		motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
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
		
		float cameraRotationX = xRot * mouseSensitivity;
		
		motor.RotateCamera(cameraRotationX);

        Vector3 jumpVec = Vector3.zero;
        if(Input.GetButton("Jump") && manager.getFuelCount() > 0)
        {
            jumpVec = Vector3.up * jumpForce;
            SetJointSettings(0f);
            manager.decrementFuelCounter();
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        if(onFloor)
        {
            manager.incrementFuelCounter();
        }

        motor.Jump(jumpVec);
    }

   private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive {mode = jointMode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Floor"))
        {
            onFloor = true;
            joint.connectedAnchor = new Vector3(0, other.transform.position.y + 1, 0);
        }
        if (other.gameObject.tag.Equals("FuelPowerUp"))
        {
            Destroy(other.gameObject);
            manager.fillFuel();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Floor")
        {
            onFloor = false;
            print("Floor");
            joint.connectedAnchor = new Vector3(0, 1, 0);
        }
    }

}
