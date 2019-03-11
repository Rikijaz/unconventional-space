using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    [SerializeField] private GameObject player_;
    [SerializeField] private GameObject ball_1_;
    [SerializeField] private GameObject ball_2_;
    [SerializeField] private GameObject ball_3_;

    private float ball_1_dist_ = 0;
    private float ball_2_dist_ = 0;
    private float ball_3_dist_ = 0;

    float ball_1_radius_ = 0.5f;
    float ball_2_radius_ = 0.5f;
    float ball_3_radius_ = 0.5f;

    // Use this for initialization
    void Start () {
        Debug.Log(ball_1_radius_);
    }
	
	// Update is called once per frame
	void Update () {
        CalculateDistance();
        AdjustBallScale();
    }

    //global_blue_ball1_ = (GameObject.Find("StimulusManager").transform.position + blue_ball1_.transform.localPosition);
    //        global_blue_ball2_ = (GameObject.Find("StimulusManager").transform.position + blue_ball2_.transform.localPosition);
    //        global_red_ball_ = (GameObject.Find("StimulusManager").transform.position + red_ball_.transform.localPosition);
    //        global_camera_ = (this.transform.position + camera_.transform.localPosition);
    //distanceBB1_ = Vector3.Distance(global_blue_ball1_, global_camera_);
    //        distanceBB2_ = Vector3.Distance(global_blue_ball2_, global_camera_);
    //        distanceR_ = Vector3.Distance(global_red_ball_, global_camera_);

    //        red_ratio_ = RED_RADIUS_ / distanceR_;
    //        blue_radius1_ = red_ratio_* distanceBB1_;
    //blue_radius2_ = red_ratio_* distanceBB2_;
    //blue_ball1_.transform.localScale = new Vector3(blue_radius1_, blue_radius1_, blue_radius1_);
    //blue_ball2_.transform.localScale = new Vector3(blue_radius2_, blue_radius2_, blue_radius2_);

    private void CalculateDistance()
    {
        
        ball_1_dist_ = Vector3.Distance(ball_1_.transform.localPosition, player_.transform.position);
        ball_2_dist_ = Vector3.Distance(ball_2_.transform.localPosition, player_.transform.position);
        ball_3_dist_ = Vector3.Distance(ball_3_.transform.localPosition, player_.transform.position);
    }

    private void AdjustBallScale()
    {
        float scale_ratio_ = ball_2_radius_ / ball_2_dist_;
        float ball_1_scale = ball_1_dist_ * scale_ratio_;
        float ball_3_scale = ball_3_dist_ * scale_ratio_;

        ball_1_.transform.localScale = new Vector3(ball_1_scale, ball_1_scale, ball_1_scale);
        ball_3_.transform.localScale = new Vector3(ball_3_scale, ball_3_scale, ball_3_scale);
    }

    
}
