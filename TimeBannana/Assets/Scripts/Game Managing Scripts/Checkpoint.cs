using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {

	private GameObject Player;
	public Sprite CheckON;
	private SpriteRenderer CheckIMAGE;
	private Game_Manager CheckScript;
	public Vector3 checkpointPOS;
	private List<GameObject> CheckList;
	private bool hasActivated = false;

	void Start () {
		Player = GameObject.FindWithTag ("Player");
		CheckScript = Player.GetComponent<Game_Manager> ();
		CheckIMAGE = gameObject.GetComponent <SpriteRenderer> ();
		checkpointPOS = Player.transform.position;
		CheckList = CheckScript.CheckList;
	}

	void Update () {
			
	}

	void OnTriggerEnter (Collider other){

		if (other.gameObject.tag == "Player" && hasActivated ==false) {
			checkpointPOS = gameObject.transform.position;
			CheckScript.CheckpointPOS = checkpointPOS;
			CheckIMAGE.sprite = CheckON; 
			CheckList.Remove (gameObject);
			hasActivated = true;
		}

	}
}
