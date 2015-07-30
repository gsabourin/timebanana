using UnityEngine;
using System.Collections;

public class Clamber : MonoBehaviour {


	private GameObject LedgeOBJ;
	private GameObject Player;
	private Vector3 Intermediate;
	private Vector3 Landing;

	void Start () {

		Player = GameObject.Find ("Player_NEW");
		LedgeOBJ = gameObject;

		Intermediate = new Vector3 (LedgeOBJ.transform.position.x,LedgeOBJ.transform.position.y +0.9f,LedgeOBJ.transform.position.z);
		Landing = new Vector3 (LedgeOBJ.transform.position.x + 2f,LedgeOBJ.transform.position.y +0.8f,LedgeOBJ.transform.position.z);

	}
	void OnTriggerEnter (Collider other){
		if (other.gameObject == Player) {
			Player.transform.position = Vector3.Lerp(Player.transform.position, Intermediate, 0.8f);
			Stage1 ();
		}
	}
	IEnumerator Stage1 (){
		yield return new WaitForSeconds (0.2f);
		Player.transform.position = Vector3.Lerp (Player.transform.position, Landing, 0.1f);
	}
}
