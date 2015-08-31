using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shooting_Script : MonoBehaviour {

	public Rigidbody slow_Bullet;
	public Rigidbody fast_Bullet;
	public Rigidbody Grenade;
	public float speed =10f;
	public float GSpeed =10f;
	private bool canShootSlow =true;
	private bool canShootFast =true;
	public float chargeLevel;
	private Slider chargeSlider;
	private Text timeLock;
	private bool LtriggerPulled = false;
	private bool RtriggerPulled = false;

	void Start (){
		chargeSlider = GameObject.Find ("Charge_Level").GetComponent <Slider>();
	}
	void Update () {
		/*if (Input.GetButtonUp ("RB")) {
			Rigidbody grenade = Instantiate(Grenade,transform.position,transform.rotation) as Rigidbody;
			grenade.velocity = transform.TransformDirection (new Vector3(0,0,GSpeed));
		}
		*/

		if (Input.GetButtonDown ("Shoot_Slow") || Input.GetAxis ("Shoot_Slow") ==1) {
			if (chargeLevel < 20f) {
				chargeLevel = chargeLevel + 0.5f;
			}
			LtriggerPulled = true;
		}
		if(Input.GetButtonDown ("Shoot_Fast") || Input.GetAxis ("Shoot_Fast") ==1){
			if (chargeLevel < 20f) {
				chargeLevel = chargeLevel + 0.5f;
			}
			RtriggerPulled = true;
		}
		chargeSlider.value = chargeLevel;


	//Check for a LEFT click or Right trigger pull to shoot the SLOW pulse
		if(Input.GetButtonUp ("Shoot_Slow") || Input.GetAxis ("Shoot_Slow") ==-1 && LtriggerPulled == true && canShootSlow == true){
			canShootSlow = false;
			Rigidbody instantiateProjectile = Instantiate(slow_Bullet,transform.position,transform.rotation) as Rigidbody;
			instantiateProjectile.velocity = transform.TransformDirection (new Vector3(0,0,speed));
			StartCoroutine (BulletWaitSlow ());
			LtriggerPulled = false;
		}
	//Check for a RIGHT click or LEFT trigger pull to shoot the FAST pulse
		if(Input.GetButtonUp ("Shoot_Fast") || Input.GetAxis ("Shoot_Fast") ==-1 && RtriggerPulled == true && canShootFast == true){
			canShootFast = false;
			Rigidbody instantiateProjectile = Instantiate(fast_Bullet,transform.position,transform.rotation) as Rigidbody;
			instantiateProjectile.velocity = transform.TransformDirection (new Vector3(0,0,speed));
			StartCoroutine (BulletWaitFast ());
			RtriggerPulled = false;
		}
	}

	void SetCharge (){

	}
	IEnumerator BulletWaitSlow (){
		 yield return new WaitForSeconds (0.2f);
			canShootSlow = true;
			chargeLevel = 0;
	}
	IEnumerator BulletWaitFast (){
		yield return new WaitForSeconds (0.2f);
			canShootFast = true;
			chargeLevel = 0;
	}
}
