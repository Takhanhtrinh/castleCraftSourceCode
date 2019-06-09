using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainScene : MonoBehaviour {

	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PlayPressed()

	{
		SceneManager.LoadScene("gameScene");
	}
	public void HowToPlayPressed()
	{

		SceneManager.LoadScene("howToPlay");
	}
	public void ReturnPressed()
	{
		SceneManager.LoadScene("mainScene");

	}
}
