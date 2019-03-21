using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityBody))]
public class PlayerSphericalGravityController : MonoBehaviour
{
    private bool kb_m_ = false;
    // public vars
    [Header("Player Stats")]
    [SerializeField] float move_speed_ = 5f;
    [SerializeField] float jump_speed_ = 400000000000000000000000000000000000f;
    [SerializeField] float rotate_speed_ = 2f;
    [SerializeField] LayerMask grounded_mask_;

    private bool grounded_;
    private Vector3 move_amount_;
    private Vector3 smooth_move_velocity_;
    private float vertical_look_rotation_;
    private Transform camera_transform_;
    private Rigidbody rigidbody_;

    public bool is_on_spring_ = false;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        camera_transform_ = Camera.main.transform;
        rigidbody_ = GetComponent<Rigidbody>();
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
        is_on_spring_ = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            kb_m_ = !kb_m_;
        }

        if (kb_m_)
        {
            ParseAndExecuteKBMInput();
        }
        else
        {
            ParseAndExecuteInput();
        }

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
        //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotate_speed_);
        //vertical_look_rotation_ += Input.GetAxis("Mouse Y") * rotate_speed_;
        //vertical_look_rotation_ = Mathf.Clamp(vertical_look_rotation_, -60, 60);
        //camera_transform_.localEulerAngles = Vector3.left * vertical_look_rotation_;

        camera_transform_.localEulerAngles = transform.localEulerAngles;


        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        Debug.Log(axis);
        float x_axis = axis.x;
        float z_axis = axis.y;

        Vector3 move_dir = new Vector3(x_axis, 0, z_axis).normalized;
        Vector3 target_movement = move_dir * move_speed_;
        move_amount_ = Vector3.SmoothDamp(move_amount_, target_movement, ref smooth_move_velocity_, .15f);


        
    }

    private void ParseAndExecuteKBMInput()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotate_speed_);
        vertical_look_rotation_ += Input.GetAxis("Mouse Y") * rotate_speed_;
        vertical_look_rotation_ = Mathf.Clamp(vertical_look_rotation_, -70, 70);
        camera_transform_.localEulerAngles = Vector3.left * vertical_look_rotation_;

        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 move_dir = new Vector3(inputX, 0, inputY).normalized;
        Vector3 target_movement = move_dir * move_speed_;
        move_amount_ = Vector3.SmoothDamp(move_amount_, target_movement, ref smooth_move_velocity_, .15f);

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