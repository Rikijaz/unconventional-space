using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour
{
    [SerializeField] float gravity_ = -9.8f;

    public void Attract(Transform body)
    {
        Vector3 gravity_up = (body.position - transform.position).normalized;
        Vector3 local_up = body.up;
        Rigidbody player_rigidbody = body.GetComponent<Rigidbody>();
        player_rigidbody.AddForce(gravity_up * gravity_);
        body.rotation = Quaternion.FromToRotation(local_up, gravity_up) * body.rotation;
    }
}