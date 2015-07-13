using UnityEngine;
using System.Collections;

public class Door_Drop_Script : MonoBehaviour {

	public GameObject Door;
	private Animator Door_Anim;
	[HideInInspector] public Game_Manager GM;
	private GameObject Player;
	// Use this for initialization
	void Start () {
		Door_Anim = Door.GetComponent <Animator> ();
		Player = GameObject.FindWithTag ("Player");
		GM = Player.GetComponent<Game_Manager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (GM.MustReset == true) {
			Door_Anim.Play("Door", -1, 0f);
			//Door_Anim.Play("Door_02", -1, 0f);
			Door_Anim.SetBool ("Start",false);
		}

	}
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == ("Player")){
			Door_Anim.SetBool ("Start",true);
		}
	}
}
