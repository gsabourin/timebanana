using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Menu_Script : MonoBehaviour {

	private HUD hudScript;

	void Start(){
		hudScript = GameObject.Find ("UI").GetComponent<HUD> ();
	}
	public void Continue(){
		hudScript.onoff = false;
		hudScript.onoffOpp = true;
	}

	public void OpenTutorial (){
		Application.LoadLevel("Tutorial");
	}
	public void OpenEasy (){
		Application.LoadLevel("SB_03_Miller");
	}
	public void OpenMedium (){
		Application.LoadLevel("SB_02_Robedeau 1");
	}
	public void OpenHard (){
		Application.LoadLevel("Brandon_Ward_Prototype");
	}
	public void OpenTest (){
		Application.LoadLevel("Test_Level");
	}
	public void OpenMainMenu (){
		Application.LoadLevel("Main_Menu");
		Time.timeScale = 1;
	}
	public void QuitGame (){
		Application.Quit();
	}
}
