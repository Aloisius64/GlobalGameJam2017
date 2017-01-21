using UnityEngine;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;


public class ScoreModality : MonoBehaviour, IGameModality {

    [SerializeField]
    private int targetScore;

    private GameObject playerOneScoreBox;
    private GameObject playerTwoScoreBox;

    private int valuePlayerOne;
    private int valuePlayerTwo;

    public ScoreModality() : base() {
        targetScore = 0;
    }

    public bool IsGameOver() {

        valuePlayerOne = Convert.ToInt32(playerOneScoreBox.GetComponent<Text>().text);
        valuePlayerTwo = Convert.ToInt32(playerTwoScoreBox.GetComponent<Text>().text);

        return valuePlayerOne > targetScore || valuePlayerTwo > targetScore;
    }

    void Start() {
        playerOneScoreBox = GameObject.Find("ScorePlayerOne").transform.GetChild(0).gameObject;
        playerTwoScoreBox = GameObject.Find("ScorePlayerTwo").transform.GetChild(0).gameObject;
    }

    void Update() {
        //Debug.Log("ScoreModality");

        if (IsGameOver()) {
            Debug.Log("Game Over");
        }
    }

    public GameObject GetWinnerPlayer() {
        throw new NotImplementedException();
    }

    public GameObject GetLoserPlayer() {
        throw new NotImplementedException();
    }
}
