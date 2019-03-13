using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    [SerializeField] bool level_complete_ = false;
    [SerializeField] Player player_;
    [SerializeField] GameObject left_sphere_;
    [SerializeField] GameObject right_sphere_;

    private float prev_player_pos_z_;
    private bool in_hallway_ = false;
    private bool looking_at_spheres_ = false;

    // Use this for initialization
    void Start () {
        prev_player_pos_z_ = player_.transform.position.z;
        player_.SetTrigger("Level");
    }
	
	// Update is called once per frame
	void Update () {
        CheckIfPlayerIsInHallWay();
        CheckIfPlayerIsLookingAtTheSpheres();
        AdjustPlayerPos();
	}


    private void CheckIfPlayerIsInHallWay()
    {
        in_hallway_ = player_.GetTrigger();
    }

    private void CheckIfPlayerIsLookingAtTheSpheres()
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

        //Debug.Log(left_sphere_dot_product + " " + right_sphere_dot_product);

    }

    private void AdjustPlayerPos()
    {
        if (!in_hallway_)
        {
            prev_player_pos_z_ = player_.transform.position.z;
        }
        else
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
    }

    
    IEnumerator AdjustTorchPos(float z_offset)
    {
        left_sphere_.transform.position = new Vector3(
            left_sphere_.transform.position.x, 
            left_sphere_.transform.position.y, 
            left_sphere_.transform.position.z - (z_offset));
        right_sphere_.transform.position = new Vector3(
            right_sphere_.transform.position.x, 
            right_sphere_.transform.position.y, 
            right_sphere_.transform.position.z - (z_offset));
        yield return new WaitForSeconds(0.01f);
    }

}
