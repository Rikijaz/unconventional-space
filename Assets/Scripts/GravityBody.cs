using UnityEngine;
using System.Collections;

public class GravityBody : MonoBehaviour
{
    Rigidbody rigidbody_;
    [SerializeField] GameObject planet_;
    GravityAttractor planet_script_;

    void Awake()
    {
        planet_script_ = planet_.GetComponent<GravityAttractor>();
        rigidbody_ = GetComponent<Rigidbody>();
        rigidbody_.useGravity = false;
        rigidbody_.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        planet_script_.Attract(transform);
    }
}