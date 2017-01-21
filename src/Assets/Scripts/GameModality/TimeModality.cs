using UnityEngine;
using System.Collections;
using System;


public class TimeModality : MonoBehaviour, IGameModality {
    [SerializeField]
    private float time;

    public TimeModality() : base() {
        time = 0.0f;
    }

    public bool IsGameOver() {
        return time > Time.timeSinceLevelLoad;
    }

    void Update() {
        Debug.Log("TimeModality");
    }

    public GameObject GetLoserPlayer() {
        throw new NotImplementedException();
    }

    public GameObject GetWinnerPlayer() {
        throw new NotImplementedException();
    }

}
