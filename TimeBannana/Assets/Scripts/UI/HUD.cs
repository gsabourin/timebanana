using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	private GameObject Timer;
	private float time;
	private int Seconds;
	private int Minutes;
	private Text timeText;


	// Use this for initialization
	void Start () {
		Timer = GameObject.Find ("Timer");
		timeText = Timer.GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		//time += Time.time;
		//timeText.text = ("Time:" + time.ToString("F2"));

	}
}
