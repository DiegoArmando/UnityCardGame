using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    float halfSec;
	float totalTime;

	int xPos;
	int zPos;

	// Use this for initialization
	void Start () {
		totalTime = 0;
        halfSec = totalTime + 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;
	
	}

	public void setPos(object[] pos)
	{
		xPos = (int)pos[0];
		zPos = (int)pos[1];
	}

    void OnMouseOver()
    {
        if(Input.GetMouseButton(0))
        {
            if (totalTime > halfSec)
            {
                transform.position = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
                halfSec = totalTime + 0.5f;
                //changes height in game manager memory
                ((GameManager)GameObject.Find("GameManager").GetComponent("GameManager")).ChangeHeight(xPos, zPos, 1);
            }
			else
			{
				print(Time.deltaTime.ToString() + " " + halfSec.ToString());
			}

        }
        else if(Input.GetMouseButton(1))
        {
            if (totalTime > halfSec)
            {
                transform.position = transform.position + new Vector3(0.0f, -0.5f, 0.0f);
                halfSec = totalTime + 0.5f;
                //changes height in game manager memory
                ((GameManager)GameObject.Find("GameManager").GetComponent("GameManager")).ChangeHeight(xPos, zPos, -1);
            }
        }
    }


    //void OnMouseDown()
    //{
      //  print(transform.position.y);
       // transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        
    //}
}
