using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadWarp : MonoBehaviour {

	private GameObject Player;
	private Player PlayerScript;

	// Use this for initialization
	void Awake () {
		Player = GameObject.Find("Player");
		PlayerScript = Player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerScript.IsTriggering(this.gameObject))
		{
			Player.transform.position = new Vector3 (0, 150, 0);
		}
	}
}
