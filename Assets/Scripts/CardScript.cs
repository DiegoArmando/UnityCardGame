//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {
	//material information for selection highlight
	Renderer rend;
	Material mat;
	
	public int Type;
	public int unitType; // 0 = Dummy; 1 = Brute; 2 = Miner; 3 = Scout; 4 = Go Up Unit
	public int spellType;

	//bool determines if card is selected
	public bool in_hand;
	public bool is_selected;

	//this gameObject's position in hand
	private Vector3 handPos;

	public GameObject highlight;


	void Start() {

		//rend = GetComponent<Renderer>();
		//mat = rend.material;
		is_selected = false;
	}

	public void setCard(int sentType, int which){
		Type = sentType;
		if (Type == 0)
		{
			unitType = which;
		} 
		else
		{
			spellType = which;
		}
		//this is called when the deck is instantiated to give this card its properties

	}

	void OnMouseEnter(){
		//card moves toward camera when hovered over
		if (in_hand) {
			handPos = gameObject.transform.position;
			gameObject.transform.position = gameObject.transform.position + (Camera.main.transform.forward * -2) + new Vector3(0,0,1.5f);
		}
	}
	
	void OnMouseExit(){
		//card returns to normal hand position when hoverover is done
		if (in_hand) {
			gameObject.transform.position = handPos;
		}
	}
	
	void OnMouseDown(){
		//	sets card as selected; will be picked up by the hand script
		if (in_hand) {
			is_selected = true;
			/*GameObject temp;
			temp = (GameObject)Instantiate(highlight);
			Vector3 offset = new Vector3(0, 0.01f, 0);
			temp.transform.position = gameObject.transform.position + offset;*/
		}
	}
}
