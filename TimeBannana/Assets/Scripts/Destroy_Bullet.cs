using UnityEngine;
using System.Collections;

public class Destroy_Bullet : MonoBehaviour {

	private GameObject Projectile;
	private Shooting_Script shootScript;
	public float chargeLevel;

	// Use this for initialization
	void Start () {
		Projectile = gameObject;
		shootScript = GameObject.Find ("SpawnPoint").GetComponent <Shooting_Script> ();
		chargeLevel = shootScript.chargeLevel;
		//gameObject.transform.localScale = new Vector3 (0.3f *chargeLevel *0.1f,0.3f *chargeLevel *0.1f,0.3f *chargeLevel *0.1f);
	}
	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == ("Player")) {

		} else {
			Destroy (Projectile);
		}
	}

	/* IEnumerator Wait(){
		yield return new WaitForSeconds (20f);
		Destroy (Projectile);
	}
	*/
}
