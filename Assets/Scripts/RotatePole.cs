using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePole : MonoBehaviour {

	public float speed = 10f;
  public bool collisioned = false;
	// Update is called once per frame
	void Update () {
	 if(!collisioned){
		   transform.Rotate(new Vector3(0,speed*Time.deltaTime,0));
		 }
	}
}
