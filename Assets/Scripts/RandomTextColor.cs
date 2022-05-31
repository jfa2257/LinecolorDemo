using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class RandomTextColor : MonoBehaviour {

	public float min;
	public float max;
	public Color defeatColor;

public void AnimateColor()
{
	if(gameObject.activeInHierarchy)
	StartCoroutine(UpdateColor());
}

public void ApplyLosingColor()
{
	GetComponent<Text>().color = defeatColor;
}

public IEnumerator UpdateColor () {
  float lerpValue = 0;
	Color startingColor = GetComponent<Text>().color;
	Color freshColor = new Color (Random.Range(min, max),Random.Range(min, max),Random.Range(min, max));
	while(gameObject.activeInHierarchy){
		if(lerpValue == 0){
			 lerpValue += Time.deltaTime;
     		 startingColor = GetComponent<Text>().color;
			 freshColor = new Color (Random.Range(min, max),Random.Range(min, max),Random.Range(min, max));
		 } else if (lerpValue < 1){
			 lerpValue += Time.deltaTime;
			 GetComponent<Text>().color = Color.Lerp(startingColor, freshColor,lerpValue);
		 } else {
			 lerpValue = 0;
		 }
			 yield return null;
		 }
	}
}
