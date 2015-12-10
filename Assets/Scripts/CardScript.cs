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
    public bool discarded = false;

	//this gameObject's position in hand
	private Vector3 handPos;

    private GameManager gm;


	void Start() {

		//rend = GetComponent<Renderer>();
		//mat = rend.material;
		is_selected = false;
        gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
	}

    void Update()
    {
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
            gameObject.transform.position += new Vector3(0, 0.1f, 0);
		}
	}
	
	void OnMouseExit(){
		//card returns to normal hand position when hoverover is done
		if (in_hand) {
            gameObject.transform.position -=  new Vector3(0, 0.1f, 0);
		}
	}
	
	void OnMouseDown(){
		//	sets card as selected; will be picked up by the hand script
        if (in_hand && !discarded)
        {
            HandScript hand = (HandScript)GameObject.Find("P"+gm.GetWhoseTurn()+"Hand").GetComponent("HandScript");
            if (!is_selected)
            {
                hand.deselect();
                is_selected = true;
                handPos = gameObject.transform.position;
                gameObject.transform.position = gameObject.transform.position + (Camera.main.transform.forward * -2) + new Vector3(0, 0, 1.5f);
                gm.ShowDBMessage(Type, unitType, spellType);
            }
            else if (is_selected)
            {
                is_selected = false;
                gameObject.transform.position = handPos;
                handPos = gameObject.transform.position;
                gm.HideDBMessage();
            }
        }
	}
}
