using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public void PlayButton(int scene) {
        Application.LoadLevel(scene);
    }

    public void ExitButton() {
        Application.Quit();
    }

    public void SetGameMode(int gameMode) {
        GameModality.GameMode = (GameMode) gameMode;
    }
}
