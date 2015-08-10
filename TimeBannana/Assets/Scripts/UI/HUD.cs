using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	private GameObject Timer;
	private float timer;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

	}
}
