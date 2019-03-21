using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (GravityBody))]
public class FirstPersonController : MonoBehaviour {

    // public vars
    [Header("Player Stats")]
    [SerializeField] float move_speed_ = 5f;
    [SerializeField] float jump_speed_ = 40f;
    [SerializeField] float rotate_speed_ = 2f;
    [SerializeField] LayerMask grounded_mask_;

    private bool grounded_;
    private Vector3 move_amount_;
    private Vector3 smooth_move_velocity_;
    private float vertical_look_rotation_;
    private Transform camera_transform_;
    private Rigidbody rigidbody_;
	
	
	void Start() {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        camera_transform_ = Camera.main.transform;
        rigidbody_ = GetComponent<Rigidbody> ();
	}


    void Update()
    {
        ParseAndExecuteInput();
        CheckIfGrounded();
    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody
        Vector3 local_move = transform.TransformDirection(move_amount_) * Time.fixedDeltaTime;
        rigidbody_.MovePosition(rigidbody_.position + local_move);
    }

    private void ParseAndExecuteInput()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotate_speed_);
        vertical_look_rotation_ += Input.GetAxis("Mouse Y") * rotate_speed_;
        vertical_look_rotation_ = Mathf.Clamp(vertical_look_rotation_, -60, 60);
        camera_transform_.localEulerAngles = Vector3.left * vertical_look_rotation_;


        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 move_dir = new Vector3(inputX, 0, inputY).normalized;
        Vector3 target_movement = move_dir * move_speed_;
        move_amount_ = Vector3.SmoothDamp(move_amount_, target_movement, ref smooth_move_velocity_, .15f);

        // Jump
        if (Input.GetButton("Jump"))
        {
            if (grounded_)
            {
                Debug.Log("Jump");
                rigidbody_.AddForce(transform.up * jump_speed_);
            }
            else
            {
                Debug.Log("d");
            }
        }
    }


    private void CheckIfGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1 + .1f, grounded_mask_))
        {
            grounded_ = true;
        }
        else
        {
            grounded_ = false;
        }
    }
}
