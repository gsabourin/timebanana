using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Menu_Script : MonoBehaviour {

	private HUD hudScript;
	private FirstPersonController playerScript;

	void Start(){
		hudScript = GameObject.Find ("UI").GetComponent<HUD> ();
		playerScript = GameObject.FindWithTag ("Player").GetComponent<FirstPersonController> ();
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
	public void LowSens (){
		playerScript.m_MouseLook.XSensitivity = 2;
		playerScript.m_MouseLook.YSensitivity = 2;
	}
	public void MedSens (){
		playerScript.m_MouseLook.XSensitivity = 5;
		playerScript.m_MouseLook.YSensitivity = 5;
	}
	public void HighSens (){
		playerScript.m_MouseLook.XSensitivity = 8;
		playerScript.m_MouseLook.YSensitivity = 8;
	}
}
