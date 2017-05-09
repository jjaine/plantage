using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTurn : MonoBehaviour {

	public static bool play = false;
	bool mergeDone = false;
	public Plant[] prefabs;

	[Header("Button stuff")]
	public Button button;
	public Sprite pauseButton;
	public Sprite playButton;
	List<GameObject> flowersInGame;
 
 	[System.Serializable]
    public class Plant
    {
        public GameObject Flower;
        public GameObject Leaf;
    }

 	// Use this for initialization
	void Start () {
		flowersInGame = new List<GameObject>();
		foreach (Transform child in transform){
            flowersInGame.Add(child.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(play){
			//instantiate new things

			//check collisions
			foreach (GameObject flower in flowersInGame){
				if(flower.GetComponent<CollideCheck>().collides){
					//merge!
					Debug.Log("MERGE");
					StartCoroutine(Merge());
				}
			}

			//all done, next turn
			if(mergeDone){
				play = false;
				button.image.sprite = playButton;
				mergeDone = false;
			}

		}
	}

	public void Play(){
		button.image.sprite = pauseButton;
		play = true;
	}

	IEnumerator Merge(){
        yield return new WaitForSeconds(5);
        mergeDone = true;
	}
}
