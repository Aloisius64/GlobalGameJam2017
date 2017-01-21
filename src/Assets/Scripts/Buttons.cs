using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}


    public void PlayButton(int scene) {
        Application.LoadLevel(scene);
    }

    public void ExitButton() {
        Application.Quit();
    }
}
