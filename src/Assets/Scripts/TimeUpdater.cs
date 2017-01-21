using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpdater : MonoBehaviour {

    [SerializeField]
    private Text timeValue;

    // Use this for initialization
    void Start () {
        timeValue = GameObject.Find("TimeBox").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        float time = Time.timeSinceLevelLoad;
        int mm = (int) time / 60;
        int ss = (int) time % 60;

        timeValue.text = mm + ":" + ss;
    }
}
