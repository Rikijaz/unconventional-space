using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardTrigger : MonoBehaviour {
	[SerializeField] HallwayGameMaster game_master_;
	[SerializeField] BoxCollider first_collider_;

	private GameObject Player;
	private Player PlayerScript;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
		PlayerScript = Player.GetComponent<Player>();
		first_collider_.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		CheckTrigger();
	}

	private void CheckTrigger()
	{
		if (PlayerScript.IsTriggering(this.gameObject)) 
		{
			game_master_.in_hallway_forward_ = true;
			first_collider_.enabled = true;
		}
		else 
		{
			game_master_.in_hallway_forward_ = false;
		}
	}
}
