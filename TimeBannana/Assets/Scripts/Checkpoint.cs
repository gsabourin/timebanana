using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	private GameObject Player;
	public Sprite CheckON;
	private SpriteRenderer CheckIMAGE;
	private Game_Manager CheckScript;
	public Vector3 checkpointPOS;

	void Start () {

		Player = GameObject.FindWithTag ("Player");
		CheckScript = Player.GetComponent<Game_Manager> ();
		CheckIMAGE = gameObject.GetComponent <SpriteRenderer> ();
		checkpointPOS = Player.transform.position;
	}

	void Update () {

	}

	void OnTriggerEnter (Collider other){

		if (other.gameObject.tag == "Player") {
			checkpointPOS = gameObject.transform.position;
			CheckScript.CheckpointPOS = checkpointPOS;
			CheckIMAGE.sprite = CheckON; 
		}
	}
}
