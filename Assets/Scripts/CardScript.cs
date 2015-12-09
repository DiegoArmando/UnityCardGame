//Developed by Robert Eldredge

using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {
	//material information for selection highlight
	Renderer rend;
	Material mat;

	//identity of the card is either a Champion or a Spell
	public enum typeEnum {
		Champion=1,
		Spell=2
	}

	//public enum 


	public int unitType; // 0 = Dummy; 1 = Brute; 2 = Miner; 3 = Scout
	public int spellType;

	//public typeEnum cardType;


	//bool determines if card is selected
	private bool is_selected;

	//return functions
	public int Type;

	public bool Selected { get { return is_selected; } }	


	void Start() {
		//receiving card art info ****TO DO****
		//string assetName = string.Format("Card");  //"Card_{0}_{1}", suit, rank
		//GameObject asset = GameObject.Find(assetName);
		//if (asset == null) {
		//	Debug.LogError("Asset '" + assetName + "' could not be found.");
		//} else {
		//	_card = Instantiate(asset, position, rotation);
		//cardType = typeEnum.Champion;
		//unitType = 0;
		//}
		rend = GetComponent<Renderer>();
		mat = rend.material;
		is_selected = false;
	}

	void setCard(int sentType, int which){
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
