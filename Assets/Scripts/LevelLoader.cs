using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ReloadLevel()
	{
		int level = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(level);
	}
}
