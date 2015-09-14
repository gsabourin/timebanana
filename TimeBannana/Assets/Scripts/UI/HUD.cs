using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	private GameObject Timer;
	public float timer;
	private int Seconds;
	private int Minutes;
	private Text timeText;

	//private Slider ghostSlider;
	
	//private FirstPersonController playerScript;
	private GameObject hudPanel;
	public GameObject Pause_Menu;
	public bool onoff= false;
	public bool onoffOpp= true;
	private Text levelBestText;
	public float levelBest;
	private Game_Manager managerScript;
	private BlurOptimized blurScript;

	// Use this for initialization
	void Start () {
		Timer = GameObject.Find ("Timer");
		timeText = Timer.GetComponent <Text> ();
		blurScript = GameObject.Find ("FirstPersonCharacter").GetComponent<BlurOptimized> ();
		hudPanel = GameObject.Find ("HUD");
		managerScript = GameObject.Find ("New_Player").GetComponent<Game_Manager> ();
		levelBestText = GameObject.Find ("BestText").GetComponent<Text>();
		levelBestText.text = levelBest.ToString();
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
		if (levelBest > timer) {
		} else {
			levelBest = timer;
		}
		if (managerScript.currentLVBest < timer) {
			levelBestText.text = timeText.text;
		} else {
		}
	}
}
