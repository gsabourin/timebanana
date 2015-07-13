using UnityEngine;
using System.Collections;

public class Shooting_Script : MonoBehaviour {

	public Rigidbody slow_Bullet;
	public Rigidbody fast_Bullet;
	public float speed =10f;
	private bool canShootSlow =true;
	private bool canShootFast =true;
	
	void Update () {
	
	//Check for a LEFT click or Right trigger pull to shoot the SLOW pulse
		if(Input.GetButtonDown ("Shoot_Slow") || Input.GetAxis ("Shoot_Slow") >0 && canShootSlow == true){
			canShootSlow = false;

			Rigidbody instantiateProjectile = Instantiate(slow_Bullet,transform.position,transform.rotation) as Rigidbody;

			instantiateProjectile.velocity = transform.TransformDirection (new Vector3(0,0,speed));

			StartCoroutine (BulletWaitSlow ());

		}
	//Check for a RIGHT click or LEFT trigger pull to shoot the FAST pulse
		if(Input.GetButtonDown ("Shoot_Fast") || Input.GetAxis ("Shoot_Fast") >0 && canShootFast == true){
			canShootFast = false;
			
			Rigidbody instantiateProjectile = Instantiate(fast_Bullet,transform.position,transform.rotation) as Rigidbody;
			
			instantiateProjectile.velocity = transform.TransformDirection (new Vector3(0,0,speed));
			
			StartCoroutine (BulletWaitFast ());
			
		}
	}
	IEnumerator BulletWaitSlow (){

		 yield return new WaitForSeconds (0.2f);
			canShootSlow = true;
	}
	IEnumerator BulletWaitFast (){
		
		yield return new WaitForSeconds (0.2f);
		canShootFast = true;
	}
}
