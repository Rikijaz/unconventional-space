using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodWarp : MonoBehaviour {

	private GameObject Player;
	private Player PlayerScript;
	private bool Hit = false;

	// Use this for initialization
	void Awake () {
		Player = GameObject.Find("Player");
		PlayerScript = Player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerScript.IsTriggering(this.gameObject))
		{
			Hit = true;	
		}
		if (Hit && Player.transform.position.y < -90)
		{
			Player.transform.position = new Vector3 (0, 150, 80);
			Hit = false;
		}
	}
}
