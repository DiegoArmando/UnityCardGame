//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {
	//material information for selection highlight
	Renderer rend;
	Material mat;
	
	public int Type;
	public int unitType; // 0 = Dummy; 1 = Brute; 2 = Miner; 3 = Scout
	public int spellType;

	//bool determines if card is selected
	public bool is_selected;

	//return functions
	//public bool Selected { get { return is_selected; } }	


	void Start() {

		rend = GetComponent<Renderer>();
		mat = rend.material;
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
		//highlights when mouse hovers over ****COSMETIC****
		//Debug.Log ("AHHH");
		//mat.SetColor("_EmissionColor", Color.grey);
	}
	void OnMouseExit(){
		//removes highlight when exiting ****COSMETIC****
		//Debug.Log ("ah");
		//mat.SetColor ("_EmissionColor", Color.black);
	}

	void OnMouseDown(){
	//	sets card as selected; will be picked up by the hand script
		is_selected = true;
	}

}
