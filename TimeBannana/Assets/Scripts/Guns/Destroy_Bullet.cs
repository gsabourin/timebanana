using UnityEngine;
using System.Collections;

public class Destroy_Bullet : MonoBehaviour {

	private GameObject Projectile;
	private Shooting_Script shootScript;
	public float chargeLevel;
	private ParticleSystem particles;

	// Use this for initialization
	void Start () {
		Projectile = gameObject;
		shootScript = GameObject.Find ("SpawnPoint").GetComponent <Shooting_Script> ();
		chargeLevel = shootScript.chargeLevel;
		particles = gameObject.GetComponent <ParticleSystem> ();

		//gameObject.transform.localScale = new Vector3 (0.3f *chargeLevel *0.05f,0.3f *chargeLevel *0.05f,0.3f *chargeLevel *0.05f);
	}
	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == ("Player")) {

		} else {
			particles.Play (true);
			StartCoroutine (Wait ());
		}
	}

	 IEnumerator Wait(){
		yield return new WaitForSeconds (0.1f);
		particles.Play (false);
		Destroy (Projectile);
	}
}
