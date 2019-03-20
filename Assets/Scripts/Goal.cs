using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    [SerializeField] GameObject player_;
    private Player player_script_;
    private ParticleSystem emitter_;
    private bool player_has_collided_with_goal_ = false;

    // Use this for initialization
    void Start () {
        player_script_ = player_.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerCollidedWithGoal())
        {
            player_has_collided_with_goal_ = true;
        }
    }

    public bool PlayerHasCollidedWithGoal()
    {
        return player_has_collided_with_goal_;
    }

    private bool PlayerCollidedWithGoal()
    {
        if (player_script_.IsTriggering(gameObject))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
