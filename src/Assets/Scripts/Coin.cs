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
    private AudioClip positiveCoinClip;
    [SerializeField]
    private AudioClip negativeCoinClip;
    [SerializeField]
    private AudioSource audioPlayer;

    private Player playerOne;
    private Player playerTwo;

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag.Contains("Player")) {
            StartCoroutine(PickupCoin(coll));
        }
    }

    // Use this for initialization
    void Start() {
        manager = GameObject.FindGameObjectWithTag("Manager");
        assetsPool = manager.GetComponent<AssetsPool>();
        audioPlayer = GetComponent<AudioSource>();

        playerOneScoreBox = GameObject.Find("ScorePlayerOne").transform.GetChild(0).gameObject;
        playerTwoScoreBox = GameObject.Find("ScorePlayerTwo").transform.GetChild(0).gameObject;

        playerOne = GameObject.FindGameObjectWithTag("PlayerSX").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerDX").GetComponent<Player>();
    }

    private IEnumerator PickupCoin(Collision2D coll) {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        // Manage score
        if (coll.collider.tag.Contains("SX")) {
            int value = Convert.ToInt32(playerOneScoreBox.GetComponent<Text>().text);
            if (gameObject.tag.Contains("SX")) {
                value++;
                playerOne.IncreaseScore();
                // Play sound
                audioPlayer.PlayOneShot(positiveCoinClip);
            } else {
                value--;
                playerOne.DecreaseScore();
                // Play sound
                audioPlayer.PlayOneShot(negativeCoinClip);
            }
            playerOneScoreBox.GetComponent<Text>().text = value + "";
        } else if (coll.collider.tag.Contains("DX")) {
            int value = Convert.ToInt32(playerTwoScoreBox.GetComponent<Text>().text);
            if (gameObject.tag.Contains("DX")) {
                value++;
                playerTwo.IncreaseScore();
                // Play sound
                audioPlayer.PlayOneShot(positiveCoinClip);
            } else {
                value--;
                playerTwo.DecreaseScore();
                // Play sound
                audioPlayer.PlayOneShot(negativeCoinClip);
            }
            playerTwoScoreBox.GetComponent<Text>().text = value + "";
        }

        yield return new WaitForSeconds(1.8f);

        if (gameObject.tag.Contains("SX")) {
            assetsPool.FreeObjectPool(eObjectType.COIN_SX, gameObject);
        } else if (gameObject.tag.Contains("DX")) {
            assetsPool.FreeObjectPool(eObjectType.COIN_DX, gameObject);
        }

      }
}
