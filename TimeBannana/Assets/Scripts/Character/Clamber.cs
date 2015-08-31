using UnityEngine;
using System.Collections;

public class Clamber : MonoBehaviour {


	private GameObject Player;
	private FirstPersonController playerController;

	//private GameObject LedgeOBJ;
	//private Vector3 Intermediate;
	//private Vector3 Landing;

	void Start () {

		Player = GameObject.FindGameObjectWithTag ("Player");
		playerController = Player.GetComponent<FirstPersonController>();

		// Used for manual transform of character (Legacy mode for clamber)
		//LedgeOBJ = gameObject;
		//Intermediate = new Vector3 (LedgeOBJ.transform.position.x,LedgeOBJ.transform.position.y +0.9f,LedgeOBJ.transform.position.z);
		//Landing = new Vector3 (LedgeOBJ.transform.position.x + 2f,LedgeOBJ.transform.position.y +0.8f,LedgeOBJ.transform.position.z);

	}
	void OnTriggerStay (Collider other){
		if (other.gameObject == Player) {
			if(Input.GetAxis ("Vertical")>0){
				playerController.useGravity = false;
				playerController.m_WalkSpeed = 1f;
				playerController.m_RunSpeed = 1f;
			}
		}
	}
	void OnTriggerExit (Collider other){
		playerController.useGravity = true;
		playerController.m_WalkSpeed = 5;
		playerController.m_RunSpeed = 6;
	}
	/* Used for manual transform of character (Legacy mode for clamber)
	IEnumerator Stage1 (){
		yield return new WaitForSeconds (0.8f);
		Player.transform.position = Vector3.Lerp (Player.transform.position, Landing, Time.deltaTime);
	}
	*/
}
