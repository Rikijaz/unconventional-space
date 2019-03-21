using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {
    [SerializeField] GameObject player_;

    private float spring_speed_ = 100;

    private PlayerSphericalGravityController player_script_;
    private Rigidbody player_rigidbody_;

	// Use this for initialization
	void Start () {
        player_script_ = player_.GetComponent<PlayerSphericalGravityController>();
        player_rigidbody_ = player_.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (player_ == collision.gameObject)
        {
            Debug.Log("hit");
            player_script_.is_on_spring_ = true;
        }
    }
}
