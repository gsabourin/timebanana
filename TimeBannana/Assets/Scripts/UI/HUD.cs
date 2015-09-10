using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	private GameObject Timer;
	private float timer;
	private int Seconds;
	private int Minutes;
	private Text timeText;

	private Slider ghostSlider;
	
	private FirstPersonController playerScript;
	private GameObject hudPanel;
	public GameObject Pause_Menu;
	public bool onoff= false;
	public bool onoffOpp= true;

	private BlurOptimized blurScript;

	// Use this for initialization
	void Start () {
		Timer = GameObject.Find ("Timer");
		timeText = Timer.GetComponent <Text> ();
		playerScript = GameObject.FindWithTag ("Player").GetComponent<FirstPersonController> ();
		blurScript = GameObject.Find ("FirstPersonCharacter").GetComponent<BlurOptimized> ();
		ghostSlider = GameObject.Find ("Ghost_Slider").GetComponent<Slider>();
		hudPanel = GameObject.Find ("HUD");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("ESC")) {
			onoff = !onoff;
			onoffOpp = !onoffOpp;
		}

		Pause_Menu.SetActive (onoff);
		blurScript.enabled = onoff;
		hudPanel.SetActive (onoffOpp);
		if(!onoff){
			Time.timeScale = 1;
		}else{
			Time.timeScale = 0;
		}

		timer += Time.deltaTime;

		int minutes = Mathf.FloorToInt(timer / 60F);
		int seconds = Mathf.FloorToInt(timer - minutes * 60);
		
		timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
	}
}
