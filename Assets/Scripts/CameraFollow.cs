using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

  public Transform target;
	public Vector3 initPosition;
	public float zOffset;
	public float xOffset;
	public float zoomPower = 5f;
	public float initFOV;
	public bool winnerOnStage = false;

public Vector3 GetUpdatedTrackPosition()
{
	return new Vector3 (target.position.x - xOffset,transform.position.y,target.position.z - zOffset);
}

 private void Start()
 {
	 initFOV = GetComponent<Camera>().fieldOfView;
     initPosition = transform.position;
 }

	void Update () {
      transform.position = GetUpdatedTrackPosition();
			if(winnerOnStage &&  GetComponent<Camera>().fieldOfView < 99){
			  GetComponent<Camera>().fieldOfView += Time.deltaTime + zoomPower;
		  }
	}

	public void RestartFollow()
	{
		GetComponent<Camera>().fieldOfView = initFOV;
		transform.position = initPosition;
	}
}
