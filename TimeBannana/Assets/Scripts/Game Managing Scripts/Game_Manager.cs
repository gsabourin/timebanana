using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Game_Manager : MonoBehaviour {

	private GameObject Player;
	private bool Death = false;
	private bool EndGame = false;
	public Vector3 CheckpointPOS;
	public bool MustReset = false;
	private HUD hudScript;
	public List<GameObject> CheckList;
	public float currentLVBest;
	
	void Start () {
		Player = GameObject.FindWithTag ("Player");
		CheckpointPOS = Player.transform.position;
		//Pause_Menu = GameObject.Find("Pause_Menu");
		CheckList.AddRange (GameObject.FindGameObjectsWithTag ("Checkpoint"));
		hudScript = GameObject.Find ("UI").GetComponent<HUD> ();
		CheckList = CheckList.OrderBy(go=>go.name).ToList();
		LoadSaves ();
	}
	
	// Update is called once per frame
	void Update () {
		if (EndGame == true) {
			Application.LoadLevel (Application.loadedLevel);
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
			Save ();
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
	public void LoadSaves(){
		if (Application.loadedLevelName == ("Test_Level")) {
				currentLVBest = PlayerPrefs.GetFloat ("TLBest");
		}
		hudScript.levelBest = currentLVBest;
	}
	public void Save(){
		if (Application.loadedLevelName == ("Test_Level")) {
			if(hudScript.levelBest < PlayerPrefs.GetFloat ("TLBest") ){
				PlayerPrefs.SetFloat ("TLBest", hudScript.timer);
			}
		}
		PlayerPrefs.Save();
		EndGame = true;
	}
}
