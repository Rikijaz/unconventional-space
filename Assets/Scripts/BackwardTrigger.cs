using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardTrigger : MonoBehaviour {
	[SerializeField] HallwayGameMaster game_master_;
	private GameObject Player;
	private Player PlayerScript;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
		PlayerScript = Player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckTrigger();
	}

	private void CheckTrigger()
	{
		if (PlayerScript.IsTriggering(this.gameObject))
		{
			game_master_.in_hallway_backward_ = true;
		}
		else 
		{
			game_master_.in_hallway_backward_ = false;
		}
	}
}
