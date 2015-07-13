using UnityEngine;
using System.Collections;

public class On_Collision_Change : MonoBehaviour {

	private bool Change = false;
	public GameObject What_To_Change;
	public MeshRenderer WTC_Renderer;
	private Color newColor;

	void Start () {
		newColor = WTC_Renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Change == true) {
			if(newColor.a >0.4){
			newColor.a -= 0.005f;
			}
			WTC_Renderer.material.color = newColor;

		}

	}
	void OnCollisionEnter (){
		Change = true;
	}
}
