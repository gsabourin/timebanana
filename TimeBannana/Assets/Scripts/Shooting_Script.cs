using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shooting_Script : MonoBehaviour {

	public Rigidbody slow_Bullet;
	public Rigidbody fast_Bullet;
	public float speed =10f;
	private bool canShootSlow =true;
	private bool canShootFast =true;
	public float chargeLevel;
	private Slider chargeSlider;
	private Text timeLock;

	void Start (){
		chargeSlider = GameObject.Find ("Charge_Level").GetComponent <Slider>();
		timeLock = GameObject.Find ("Time_Lock").GetComponent <Text>();
	}
	void Update () {
		if (chargeLevel == 20) {
			timeLock.color = Color.green;
		} else {
			timeLock.color = Color.white;
		}
		chargeSlider.value = chargeLevel;

		if (Input.GetButtonDown ("Shoot_Slow") || Input.GetAxis ("Shoot_Slow") > 0) {
			if (chargeLevel < 20f) {
				chargeLevel = chargeLevel + 1;
			}
		}
		if(Input.GetButtonDown ("Shoot_Fast") || Input.GetAxis ("Shoot_Fast") >0){
			if (chargeLevel < 20f) {
				chargeLevel = chargeLevel + 1;
			}
		}

	//Check for a LEFT click or Right trigger pull to shoot the SLOW pulse
		if(Input.GetButtonUp ("Shoot_Slow") /*|| Input.GetAxis ("Shoot_Slow") <1*/ && canShootSlow == true){
			canShootSlow = false;
			Rigidbody instantiateProjectile = Instantiate(slow_Bullet,transform.position,transform.rotation) as Rigidbody;
			instantiateProjectile.velocity = transform.TransformDirection (new Vector3(0,0,speed));
			StartCoroutine (BulletWaitSlow ());

		}
	//Check for a RIGHT click or LEFT trigger pull to shoot the FAST pulse
		if(Input.GetButtonUp ("Shoot_Fast") /*|| Input.GetAxis ("Shoot_Fast") >0*/ && canShootFast == true){
			canShootFast = false;
			Rigidbody instantiateProjectile = Instantiate(fast_Bullet,transform.position,transform.rotation) as Rigidbody;
			instantiateProjectile.velocity = transform.TransformDirection (new Vector3(0,0,speed));
			StartCoroutine (BulletWaitFast ());
			
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
