using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

    [SerializeField]
    private GameObject playerOneScoreBox;
    [SerializeField]
    private GameObject playerTwoScoreBox;

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "Player") {
            Destroy(gameObject);

            // Manage score
            if (coll.collider.name == "Player One") {
                int value = Convert.ToInt32(playerOneScoreBox.GetComponent<Text>().text);
                value++;
                playerOneScoreBox.GetComponent<Text>().text = value + "";
            } else if (coll.collider.name == "Player Two") {
                int value = Convert.ToInt32(playerTwoScoreBox.GetComponent<Text>().text);
                value++;
                playerTwoScoreBox.GetComponent<Text>().text = value + "";
            }
        }
    }

    // Use this for initialization
    void Start() {
        playerOneScoreBox = GameObject.Find("ScorePlayerOne").transform.GetChild(0).gameObject;
        playerTwoScoreBox = GameObject.Find("ScorePlayerTwo").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update() {

    }
}
