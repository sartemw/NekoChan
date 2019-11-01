using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discoloration : MonoBehaviour {

	private Color32 _color;
	private byte i=0 ;
	
	void Update () {
		if (Input.GetKey(KeyCode.Z))
		{
			i++;
			GetComponent<SpriteRenderer>().color = new Color32(i, 0, 0, 255);			
		}
	}
}
