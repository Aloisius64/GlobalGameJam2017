using UnityEngine;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;


public class TimeScoreModality : MonoBehaviour, IGameModality {

    [SerializeField]
    private int targetScore;
    [SerializeField]
    private float targetTime;

    private GameObject playerOneScoreBox;
    private GameObject playerTwoScoreBox;

    private int valuePlayerOne;
    private int valuePlayerTwo;

    [SerializeField]
    private Text timeValue;

    float remainingTime;

    [SerializeField]
    private float thresholdTime = 30.0f;

    public bool IsGameOver() {

        valuePlayerOne = Convert.ToInt32(playerOneScoreBox.GetComponent<Text>().text);
        valuePlayerTwo = Convert.ToInt32(playerTwoScoreBox.GetComponent<Text>().text);

        return remainingTime<=0 || valuePlayerOne > targetScore || valuePlayerTwo > targetScore;
    }

    void Start() {
        playerOneScoreBox = GameObject.Find("ScorePlayerOne").transform.GetChild(0).gameObject;
        playerTwoScoreBox = GameObject.Find("ScorePlayerTwo").transform.GetChild(0).gameObject;

        timeValue = GameObject.Find("TimeBox").GetComponent<Text>();

        Debug.Log("ScoreModality");
    }

    void Update() {
        // Time update
        remainingTime = targetTime - Time.timeSinceLevelLoad;
        int mm = (int) remainingTime / 60;
        int ss = (int) remainingTime % 60;
        timeValue.text = mm + ":" + ss;

        if(remainingTime < thresholdTime) {
            timeValue.color = Color.red;
        }

        if (IsGameOver()) {
            Debug.Log("Game Over");

            Application.LoadLevel(1);
        }
    }

    public GameObject GetWinnerPlayer() {
        throw new NotImplementedException();
    }

    public GameObject GetLoserPlayer() {
        throw new NotImplementedException();
    }
}
