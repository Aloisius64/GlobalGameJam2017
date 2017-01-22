using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerUpdateUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = GameModality.Winner;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
