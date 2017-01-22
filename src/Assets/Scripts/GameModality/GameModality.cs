using UnityEngine;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {
    TWO_PLAYER = 0,
    PLAYER_VS_AI,
    AI_VS_AI
}

public class GameModality : MonoBehaviour, IGameModality {

    private static string winner;
    private static GameMode gameMode = GameMode.TWO_PLAYER;

    [SerializeField]
    private int targetScore;
    [SerializeField]
    private float targetTime;

    private GameObject playerOneScoreBox;
    private GameObject playerTwoScoreBox;

    private GameObject[] players = new GameObject[2];

    private int valuePlayerOne;
    private int valuePlayerTwo;

    [SerializeField]
    private Text timeValue;
    float remainingTime;

    [SerializeField]
    private float thresholdTime = 30.0f;

    public static string Winner {
        get {
            return winner;
        }
    }

    internal static GameMode GameMode {
        get {
            return gameMode;
        }

        set {
            gameMode = value;
        }
    }

    public bool IsGameOver() {

        valuePlayerOne = Convert.ToInt32(playerOneScoreBox.GetComponent<Text>().text);
        valuePlayerTwo = Convert.ToInt32(playerTwoScoreBox.GetComponent<Text>().text);

        if (valuePlayerOne > valuePlayerTwo) {
            winner = "Player One Wins";
        } else if (valuePlayerOne < valuePlayerTwo) {
            winner = "Player Two Wins";
        } else {
            winner = "Draw";
        }

        return remainingTime <= 0 || valuePlayerOne >= targetScore || valuePlayerTwo >= targetScore;
    }

    void Start() {
        playerOneScoreBox = GameObject.Find("ScorePlayerOne").transform.GetChild(0).gameObject;
        playerTwoScoreBox = GameObject.Find("ScorePlayerTwo").transform.GetChild(0).gameObject;

        timeValue = GameObject.Find("TimeBox").GetComponent<Text>();

        // Set Game mode
        players[0] = GameObject.FindGameObjectWithTag("PlayerSX");
        players[1] = GameObject.FindGameObjectWithTag("PlayerDX");

        switch (gameMode) {
            case GameMode.TWO_PLAYER: {
                    foreach (var item in players) {
                        item.GetComponent<PlayerController>().enabled = true;
                        item.GetComponent<AI>().enabled = false;
                    }
                }
                break;
            case GameMode.PLAYER_VS_AI: {
                    players[0].GetComponent<PlayerController>().enabled = true;
                    players[0].GetComponent<AI>().enabled = false;

                    players[1].GetComponent<PlayerController>().enabled = false;
                    players[1].GetComponent<AI>().enabled = true;
                }
                break;
            default: {
                    foreach (var item in players) {
                        item.GetComponent<PlayerController>().enabled = false;
                        item.GetComponent<AI>().enabled = true;
                    }
                }
                break;
        }
    }

    void Update() {
        // Time update
        remainingTime = targetTime - Time.timeSinceLevelLoad;
        int mm = (int)remainingTime / 60;
        int ss = (int)remainingTime % 60;
        timeValue.text = mm + ":" + ss;

        if (remainingTime < thresholdTime) {
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
