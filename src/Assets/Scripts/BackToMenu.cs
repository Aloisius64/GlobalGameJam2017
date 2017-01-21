using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour {

    // Use this for initialization
    void Start() {
        StartCoroutine(BackAction());
    }

    private IEnumerator BackAction() {
        yield return new WaitForSeconds(4.0f);
        Application.LoadLevel(0);
    }

}
