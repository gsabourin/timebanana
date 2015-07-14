﻿using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {

	private GameObject Player;
	private bool Death = false;
	private bool EndGame = false;
	public Vector3 CheckpointPOS;
	public bool MustReset = false;

	void Start () {
		Player = GameObject.FindWithTag ("Player");
		CheckpointPOS = Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (EndGame == true) {
			Application.LoadLevel ("Prototype");
			EndGame = false;
		}

		if(Input.GetButtonDown ("Back")){
			StartCoroutine (Wait());
		}

		if (Death == true) {
			Player.transform.position = CheckpointPOS;
			MustReset = false;
			Death = false;
		} else {

		}
	}
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == ("FinishLine")) {
			StartCoroutine (WaitEnd());
		}
		if (other.gameObject.tag == ("DeathObject")) {
			StartCoroutine (Wait());
		}
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (0.2f);
		Death = true;
		MustReset = true;
	}
	IEnumerator WaitEnd(){
		yield return new WaitForSeconds (0.2f);
		EndGame = true;
	}
}