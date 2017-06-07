using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTurn : MonoBehaviour {

	public static bool play = false;
	bool mergeDone = false;
	bool inMerge = false;
	public GameObject[] prefabs;
	public List<GameObject> flowersInGame;
	public GameObject explosion;

	[Header("Button stuff")]
	public Button button;
	public Sprite pauseButton;
	public Sprite playButton; 

	[Header("Map stuff")]
	public GameObject CenterCircle;
	public GameObject LeftPart;
	public GameObject TopPart;
	public GameObject RightPart;
	public GameObject LeftBar;
	public GameObject RightBar;
	public GameObject BottomBar;
	float target;

 	// Use this for initialization
	void Start () {
		flowersInGame = new List<GameObject>();
		for(int i=0; i<3; i++){
			AddNewFlower(TopPart);
			AddNewFlower(LeftPart);
			AddNewFlower(RightPart);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(play){

			//check collisions
			bool c = CheckCollisions();

			//all done, next turn
			if(mergeDone){
				foreach (GameObject flower in flowersInGame){
					if(flower!=null){
						flower.GetComponent<PlantInfo>().turnsLeft--;
						if(flower.GetComponent<PlantInfo>().turnsLeft < 2){
							Color prev = flower.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
							prev.a = 0.8f;
							flower.transform.GetChild(0).GetComponent<SpriteRenderer>().color = prev;
							flower.transform.GetChild(1).GetComponent<SpriteRenderer>().color = prev;
						}
						if(flower.GetComponent<PlantInfo>().turnsLeft < 1){
							StartCoroutine(Blinking(flower));
						}
					}
				}
				//rotate circle
				target = CenterCircle.transform.eulerAngles.z-120;

				//instantiate new things
				if(TopPart.transform.childCount < 3){
					for(int i=0; i<3-TopPart.transform.childCount; i++)
						AddNewFlower(TopPart);
					AkSoundEngine.PostEvent("Play_Spawn", gameObject);
				}
				if(LeftPart.transform.childCount < 3){
					for(int i=0; i<3-LeftPart.transform.childCount; i++)
						AddNewFlower(LeftPart);
					AkSoundEngine.PostEvent("Play_Spawn", gameObject);
				}
				if(RightPart.transform.childCount < 3){
					for(int i=0; i<3-RightPart.transform.childCount; i++)
						AddNewFlower(RightPart);
					AkSoundEngine.PostEvent("Play_Spawn", gameObject);
				}

				play = false;
				button.image.sprite = playButton;
				mergeDone = false;
				AkSoundEngine.PostEvent("Play_Spinning_Wheel", gameObject);
			}
			else if(!c){
				foreach (GameObject flower in flowersInGame){
					if(flower!=null){
						flower.GetComponent<PlantInfo>().turnsLeft--;
						if(flower.GetComponent<PlantInfo>().turnsLeft < 2){
							Color prev = flower.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
							prev.a = 0.8f;
							flower.transform.GetChild(0).GetComponent<SpriteRenderer>().color = prev;
							flower.transform.GetChild(1).GetComponent<SpriteRenderer>().color = prev;
						}
						if(flower.GetComponent<PlantInfo>().turnsLeft < 1){
							StartCoroutine(Blinking(flower));
						}
					}
				}
				//rotate circle
				target = CenterCircle.transform.eulerAngles.z-120;

				//instantiate new things
				if(TopPart.transform.childCount < 3){
					for(int i=0; i<3-TopPart.transform.childCount; i++)
						AddNewFlower(TopPart);
				}
				if(LeftPart.transform.childCount < 3){
					for(int i=0; i<3-LeftPart.transform.childCount; i++)
						AddNewFlower(LeftPart);
				}
				if(RightPart.transform.childCount < 3){
					for(int i=0; i<3-RightPart.transform.childCount; i++)
						AddNewFlower(RightPart);
				}				

				
				StartCoroutine(EndPlay());
				AkSoundEngine.PostEvent("Play_Spinning_Wheel", gameObject);
			}
		}

		CenterCircle.transform.rotation = Quaternion.Lerp(CenterCircle.transform.rotation, Quaternion.Euler(0,0,target),0.1f);

	}

	IEnumerator Blinking(GameObject flower){
		while(true && flower!=null){
	        flower.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
	        flower.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
	        yield return new WaitForSeconds(0.3f);
	        if(flower!=null){
	        	flower.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
	        	flower.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
	    	}
	        yield return new WaitForSeconds(1.5f);
	    }
    }

    IEnumerator StartPlay(){
    	yield return new WaitForSeconds(0.5f);
    	play = true;
    	LeftBar.GetComponent<Collider2D>().enabled = false;
    	RightBar.GetComponent<Collider2D>().enabled = false;
    	BottomBar.GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator EndPlay(){
		mergeDone = false;
		play = false;
    	yield return new WaitForSeconds(2f);
    	LeftBar.GetComponent<Collider2D>().enabled = true;
    	RightBar.GetComponent<Collider2D>().enabled = true;
    	BottomBar.GetComponent<Collider2D>().enabled = true;
		button.image.sprite = playButton;
    }

	public void Play(){
		button.image.sprite = pauseButton;
		AkSoundEngine.PostEvent("Play_Button", gameObject);
		StartCoroutine(StartPlay());
	}

	bool CheckCollisions(){

		Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(0, -0.71f), 1.2f);

		foreach (GameObject flower in flowersInGame){
			if(flower!=null){
				Debug.Log("Flower had turns left: " + flower.GetComponent<PlantInfo>().turnsLeft);
				if(flower.GetComponent<PlantInfo>().turnsLeft == 0){
					AkSoundEngine.PostEvent("Play_Flower_Die", gameObject);
					Debug.Log("Flower killed, flower was "+flower.GetComponent<PlantInfo>().Color + " " 
						+ flower.GetComponent<PlantInfo>().FlowerCorners + " "
						+ flower.GetComponent<PlantInfo>().LeafCorners + " at " + flower.transform.parent.transform.name);
					Destroy(flower);
				}
				else{
					for(int i=0; i<cols.Length; i++){
						if(cols[i].gameObject == flower)
							flower.transform.parent = CenterCircle.transform;
					}
					if(flower.GetComponent<Collider2D>().IsTouching(LeftPart.GetComponent<Collider2D>())){
						flower.transform.parent = LeftPart.transform;
					} else if(flower.GetComponent<Collider2D>().IsTouching(RightPart.GetComponent<Collider2D>())){
						flower.transform.parent = RightPart.transform;
					} else if(flower.GetComponent<Collider2D>().IsTouching(TopPart.GetComponent<Collider2D>())){
						flower.transform.parent = TopPart.transform;
					}

					if(!flower.transform.GetChild(0).GetComponent<CollideCheck>().inMerge && flower.transform.GetChild(0).GetComponent<CollideCheck>().collides){
						//merge!
						GameObject otherFlower = flower.transform.GetChild(0).GetComponent<CollideCheck>().otherFlower;
						if(otherFlower){
							GameObject newFlower = Combination(flower);
							StartCoroutine(Merge(flower, newFlower, otherFlower));
						}
						return true;
					}
				}
			}
		}
		if(!inMerge)
			return false;
		else
			return true;
	}

	IEnumerator Merge(GameObject flower, GameObject newFlower, GameObject otherFlower){
		inMerge = true;
		flower.transform.GetChild(0).GetComponent<CollideCheck>().inMerge = true;
		otherFlower.transform.GetChild(0).GetComponent<CollideCheck>().inMerge = true;
		AkSoundEngine.PostEvent("Play_Flower_Merge", gameObject);

        yield return new WaitForSeconds(1);
        flowersInGame.Remove(flower);
        Destroy(flower);
        flowersInGame.Remove(otherFlower);
        Destroy(otherFlower);
        flowersInGame.Add(newFlower);
        
        yield return new WaitForSeconds(1);
        mergeDone = true;
        inMerge = false;
	}

	string CombineColors(string c1, string c2){
		if(c1=="Brown" || c2=="Brown")
			return "Brown";
		else if(c1=="Pink" || c2=="Pink")
			return "Pink";
		else if(c1=="OrangeRed" || c2=="OrangeRed")
			return "OrangeRed";
		else if(c1=="OrangeYellow" || c2=="OrangeYellow")
			return "OrangeYellow";
		else if(c1=="GreenBlue" || c2=="GreenBlue")
			return "GreenBlue";
		else if(c1=="GreenYellow" || c2=="GreenYellow")
			return "GreenYellow";
		else if(c1=="VioletBlue" || c2=="VioletBlue")
			return "VioletBlue";
		else if(c1=="Red"){
			if(c2=="Red")
				return "Red";
			if(c2=="Yellow")
				return "Orange";
			if(c2=="Blue")
				return "Violet";
			if(c2=="Green")
				return "Brown";
			if(c2=="Orange")
				return "OrangeRed";
			if(c2=="Violet")
				return "Pink";
		}
		else if(c1=="Yellow"){
			if(c2=="Yellow")
				return "Yellow";
			if(c2=="Blue")
				return "Green";
			if(c2=="Red")
				return "Orange";
			if(c2=="Violet")
				return "Brown";
			if(c2=="Orange")
				return "OrangeYellow";
			if(c2=="Green")
				return "GreenYellow";
		}
		else if(c1=="Blue"){
			if(c2=="Blue")
				return "Blue";
			if(c2=="Red")
				return "Violet";
			if(c2=="Yellow")
				return "Green";
			if(c2=="Orange")
				return "Brown";
			if(c2=="Violet")
				return "VioletBlue";
			if(c2=="Green")
				return "GreenBlue";
		}
		else if(c1=="Orange"){
			if(c2=="Orange")
				return "Orange";
			if(c2=="Blue")
				return "Brown";
			if(c2=="Yellow")
				return "OrangeYellow";
			if(c2=="Red")
				return "OrangeRed";
		}
		else if(c1=="Green"){
			if(c2=="Green")
				return "Green";
			if(c2=="Red")
				return "Brown";
			if(c2=="Blue")
				return "GreenBlue";
			if(c2=="Yellow")
				return "GreenYellow";
		}
		else if(c1=="Violet"){
			if(c2=="Violet")
				return "Violet";
			if(c2=="Yellow")
				return "Brown";
			if(c2=="Red")
				return "Pink";
			if(c2=="Blue")
				return "VioletBlue";
		}
		
		return "Not found!";
	}

	GameObject Combination(GameObject flower){
		GameObject other = flower.transform.GetChild(0).GetComponent<CollideCheck>().otherFlower;
		int lCorners = flower.GetComponent<PlantInfo>().LeafCorners + other.GetComponent<PlantInfo>().LeafCorners;
		int fCorners = flower.GetComponent<PlantInfo>().FlowerCorners + other.GetComponent<PlantInfo>().FlowerCorners;
		if(lCorners < 1) lCorners = -1;
		if(lCorners > 2) lCorners = 2;
		if(fCorners < 1) fCorners = -1;
		if(fCorners > 6) fCorners = 6;
		string color = CombineColors(flower.GetComponent<PlantInfo>().Color, other.GetComponent<PlantInfo>().Color);

		int i;
		for(i=0; i<prefabs.Length; i++){
			if(prefabs[i].GetComponent<PlantInfo>().LeafCorners == lCorners && 
				prefabs[i].GetComponent<PlantInfo>().FlowerCorners == fCorners && 
				prefabs[i].GetComponent<PlantInfo>().Color == color){
				break;
			}
		}
		if(i==prefabs.Length){
			Debug.Log("Invalid merge when merging " 
				+ flower.GetComponent<PlantInfo>().Color + " " 
				+ flower.GetComponent<PlantInfo>().FlowerCorners + " "
				+ flower.GetComponent<PlantInfo>().LeafCorners + " with "
				+ other.GetComponent<PlantInfo>().Color + " " 
				+ other.GetComponent<PlantInfo>().FlowerCorners + ", "
				+ other.GetComponent<PlantInfo>().LeafCorners);
			return null;
		}
		else{
			GameObject f = Instantiate(prefabs[i], (other.transform.position+flower.transform.position)/2, Quaternion.identity);
			f.transform.parent = flower.transform.parent;
			return f;
		}
		
	}

	void AddNewFlower(GameObject part){
		int a = Random.Range(0,27);
		Vector3 pos = Vector3.zero;
		if(part.transform.name == "LeftPart")
			pos = new Vector3(Random.Range(-9.0f, -3.5f), Random.Range(-5.0f, 0.0f), -1);
		if(part.transform.name == "TopPart")
			pos = new Vector3(Random.Range(-3f, 3f), Random.Range(3f, 5f), -1);
		if(part.transform.name == "RightPart")
			pos = new Vector3(Random.Range(3.5f, 9.0f), Random.Range(-5.0f, 0.0f), -1);
		GameObject f = Instantiate(prefabs[a], pos, Quaternion.identity);
		f.transform.parent = part.transform;
		flowersInGame.Add(f);
	}
}
