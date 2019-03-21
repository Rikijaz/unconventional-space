using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGameMaster : MonoBehaviour {
    [SerializeField] bool level_complete_ = false;
    [SerializeField] Player player_;
    [SerializeField] GameObject left_sphere_;
    [SerializeField] GameObject right_sphere_;
    [SerializeField] GameObject front_fog_;
    [SerializeField] GameObject rear_fog_;

    private float prev_player_pos_z_;

    public bool in_hallway_forward_ = false;
    public bool in_hallway_backward_ = false;
    public bool looked_at_left_sphere_ = false;
    public bool looked_at_right_sphere_ = false;

    // Use this for initialization
    void Start () {
        prev_player_pos_z_ = player_.transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        if (!(looked_at_left_sphere_ && looked_at_right_sphere_))
        {
            AdjustPlayerPos();
            CheckIfPlayerIsLookingAtTheSpheres();
        }
        StartCoroutine(AdjustFogPos());
        StartCoroutine(CheckIfPlayerIsLookingAtTheSpheres());
	}

    IEnumerator CheckIfPlayerIsLookingAtTheSpheres()
    {
        Vector3 left_sphere_direction = (
            left_sphere_.transform.position -
            player_.transform.position).normalized;
        Vector3 right_sphere_direction = (
            right_sphere_.transform.position -
            player_.transform.position).normalized;

        float left_sphere_dot_product = Vector3.Dot(
            left_sphere_direction,
            player_.transform.forward);
        float right_sphere_dot_product = Vector3.Dot(
            right_sphere_direction,
            player_.transform.forward);
        float left_dist_ = Vector3.Distance(
            player_.transform.position, 
            left_sphere_.transform.position);
        float right_dist_ = Vector3.Distance(
            player_.transform.position, 
            right_sphere_.transform.position);
        Debug.Log(left_dist_ + " " + right_dist_);
        if (left_sphere_dot_product > .99f && left_dist_ < 10) 
        {
            looked_at_left_sphere_ = true;

        }
        if (right_sphere_dot_product > .99f && right_dist_ < 10) 
        {
            looked_at_right_sphere_ = true;
        }
        Debug.Log(left_sphere_dot_product + " " + right_sphere_dot_product);
        yield return new WaitForSeconds(1f);
    }

    private void AdjustPlayerPos()
    {
        if (!in_hallway_forward_ && !in_hallway_backward_)
        {
            prev_player_pos_z_ = player_.transform.position.z;
        }
        else if (in_hallway_forward_)
        {
            Vector3 curr_player_pos_ = player_.transform.position;
            if (curr_player_pos_.z > prev_player_pos_z_)
            {
                player_.transform.position = new Vector3(
                    player_.transform.position.x, 
                    player_.transform.position.y, 
                    prev_player_pos_z_);

                
                float z_offset = curr_player_pos_.z - prev_player_pos_z_;
                Debug.Log(z_offset);
                StartCoroutine(AdjustTorchPos(z_offset));
            }
            // movement = new Vector3 (deltaX, gravity, 0);
        }
        // in_hallway_backward_ is true.
        else 
        {
            Vector3 curr_player_pos_ = player_.transform.position;
            if (curr_player_pos_.z < prev_player_pos_z_)
            {
                player_.transform.position = new Vector3(
                    player_.transform.position.x, 
                    player_.transform.position.y, 
                    prev_player_pos_z_);

                
                float z_offset = curr_player_pos_.z - prev_player_pos_z_;
                Debug.Log(z_offset);
                StartCoroutine(AdjustTorchPos(z_offset));
            }
        }
    }

    
    IEnumerator AdjustTorchPos(float z_offset)
    {
        left_sphere_.transform.position = new Vector3(
            left_sphere_.transform.position.x, 
            left_sphere_.transform.position.y, 
            left_sphere_.transform.position.z - (z_offset));
        if (left_sphere_.transform.position.z < -25) 
        {
            left_sphere_.transform.position = new Vector3(
            left_sphere_.transform.position.x,
            left_sphere_.transform.position.y,
            40);
        }
        else if (left_sphere_.transform.position.z > 40) 
        {
            left_sphere_.transform.position = new Vector3(
            left_sphere_.transform.position.x,
            left_sphere_.transform.position.y,
            -25);
        }
        right_sphere_.transform.position = new Vector3(
            right_sphere_.transform.position.x, 
            right_sphere_.transform.position.y, 
            right_sphere_.transform.position.z - (z_offset));
        if (right_sphere_.transform.position.z < -25) 
        {
            right_sphere_.transform.position = new Vector3(
            right_sphere_.transform.position.x,
            right_sphere_.transform.position.y,
            40);
        }
        else if (right_sphere_.transform.position.z > 40) 
        {
            right_sphere_.transform.position = new Vector3(
            right_sphere_.transform.position.x,
            right_sphere_.transform.position.y,
            -25);
        }
        yield return new WaitForSeconds(0.01f);
    }

    IEnumerator AdjustFogPos()
    {
        front_fog_.transform.position = new Vector3(
            front_fog_.transform.position.x,
            front_fog_.transform.position.y,
            player_.transform.position.z + 30);
        rear_fog_.transform.position = new Vector3(
            rear_fog_.transform.position.x,
            rear_fog_.transform.position.y,
            player_.transform.position.z - 30);
            
        yield return new WaitForSeconds(0.01f);
    }
}
