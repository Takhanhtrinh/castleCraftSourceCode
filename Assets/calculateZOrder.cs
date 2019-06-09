using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calculateZOrder : MonoBehaviour {

	SpriteRenderer spr;
	// Use this for initialization
	void Start () {
		spr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		spr.sortingOrder = Screen.height - (int)Mathf.Ceil(Camera.main.WorldToScreenPoint(transform.position).y);
		
	}
}
