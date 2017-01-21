using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

    [SerializeField]
    private GameObject manager = null;
    private AssetsPool assetsPool = null;
    [SerializeField]
    private GameObject playerOneScoreBox;
    [SerializeField]
    private GameObject playerTwoScoreBox;
    [SerializeField]
    private AudioClip coinClip;
    [SerializeField]
    private AudioSource audioPlayer;

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag.Contains("Player")) {
            // Play sound
            //audioPlayer.PlayOneShot(coinClip);

            if (gameObject.tag.Contains("SX")) {
                assetsPool.FreeObjectPool(eObjectType.COIN_SX, gameObject);
            } else if (gameObject.tag.Contains("DX")) {
                assetsPool.FreeObjectPool(eObjectType.COIN_DX, gameObject);
            }

            // Manage score
            if (coll.collider.tag.Contains("SX")) {
                int value = Convert.ToInt32(playerOneScoreBox.GetComponent<Text>().text);
                if (gameObject.tag.Contains("SX")) {
                    value++;
                } else {
                    value--;
                }
                playerOneScoreBox.GetComponent<Text>().text = value + "";
            } else if (coll.collider.tag.Contains("DX")) {
                int value = Convert.ToInt32(playerTwoScoreBox.GetComponent<Text>().text);
                if (gameObject.tag.Contains("DX")) {
                    value++;
                } else {
                    value--;
                }
                playerTwoScoreBox.GetComponent<Text>().text = value + "";
            }
        }
    }

    // Use this for initialization
    void Start() {
        manager = GameObject.FindGameObjectWithTag("Manager");
        assetsPool = manager.GetComponent<AssetsPool>();
        audioPlayer = GetComponent<AudioSource>();

        playerOneScoreBox = GameObject.Find("ScorePlayerOne").transform.GetChild(0).gameObject;
        playerTwoScoreBox = GameObject.Find("ScorePlayerTwo").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update() {

    }
}
