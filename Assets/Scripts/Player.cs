using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;



public class Player : MonoBehaviour {
    [SerializeField] private bool kb_m_ = false;

    [Header("Player Stats")]
    [SerializeField] private float move_speed_ = 5f;
    [SerializeField] private float jump_speed_ = 5f;
    [SerializeField] private float rotate_speed_ = 2f;
    [SerializeField] private float gravity_ = 9.8f;

    private float yaw_ = 0f;
    private float pitch_ = 0f;

    private Vector3 vel_ = new Vector3(0, 0, 0);
    private CharacterController char_controller_;
    private bool is_jumping_ = false;

    public List<GameObject> objects_triggering_;

	// Use this for initialization
	void Start () {
        char_controller_ = GetComponent<CharacterController>();
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (kb_m_)
        {
            ParseAndExecuteKBMInput();
        }
        else
        {
            ParseInput();
            ExecuteInput();
        }
        
	}

    public bool IsTriggering(GameObject trigger_object)
    {
        bool is_trigger = false;
        if (objects_triggering_.Contains(trigger_object))
        {
            is_trigger = true;
        }

        return is_trigger;
    }

    private void ParseInput()
    {
        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick); 
        float x_axis = axis.x;
        float z_axis = axis.y;

        float x_vel = move_speed_ * x_axis;
        float z_vel = move_speed_ * z_axis;
        float y_vel = 0f;

        if (OVRInput.Get(OVRInput.Button.One))
        {
            if (char_controller_.isGrounded)
            {
                y_vel = jump_speed_;
            }
        }

        y_vel = y_vel - (gravity_ * Time.deltaTime);


        vel_ = new Vector3(x_vel, -gravity_, z_vel);
    }

    private void ExecuteInput()
    {
        vel_ = transform.TransformDirection(vel_);
        char_controller_.Move(vel_ * Time.deltaTime);
    }

    private void ParseAndExecuteKBMInput()
    {
        // Read mouse
        pitch_ -= rotate_speed_ * Input.GetAxis("Mouse Y");
        yaw_ += rotate_speed_ * Input.GetAxis("Mouse X");

        // Read keyboard
        float x_vel = move_speed_ * Input.GetAxis("Horizontal");
        float z_vel = move_speed_ * Input.GetAxis("Vertical");

        // Rotate player
        transform.eulerAngles = new Vector3(pitch_, yaw_, 0.0f);

        // Calculate ONLY XZ velocity from local space to world space based on rotation
        vel_ = new Vector3(x_vel, 0f, z_vel);
        vel_ = transform.TransformDirection(vel_);

        // Calculate Y velocity
        float y_vel = 0f;
        if (char_controller_.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                y_vel = jump_speed_;
            }
        }
        // Apply gravity
        y_vel = y_vel - (gravity_);

        // Apply to vel_ vector
        vel_.y = y_vel;

        // Move player
        char_controller_.Move(vel_ * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!objects_triggering_.Contains(collision.gameObject))
        {
            Debug.Log(collision.gameObject.name);
            objects_triggering_.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (objects_triggering_.Contains(collision.gameObject))
        {
            objects_triggering_.Remove(collision.gameObject);
        }
    }
}
