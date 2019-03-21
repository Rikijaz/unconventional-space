using UnityEngine;
using System.Collections;

public class GravityBody : MonoBehaviour
{
    Rigidbody rigidbody_;
    [SerializeField] GameObject planet_;
    GravityAttractor planet_script_;
    PlayerSphericalGravityController controller_script_;

    void Start()
    {
        planet_script_ = planet_.GetComponent<GravityAttractor>();
        controller_script_ = GetComponent<PlayerSphericalGravityController>();
        rigidbody_ = GetComponent<Rigidbody>();
        rigidbody_.useGravity = false;
        rigidbody_.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        //Debug.Log(transform.position + " " + planet_.transform.position);

        Debug.Log(controller_script_.is_on_spring_);
        if (!controller_script_.is_on_spring_)
        {
            planet_script_.Attract(transform);
            rigidbody_.freezeRotation = true;
        }
        else
        {
            rigidbody_.useGravity = true;
        }
        
    }
}