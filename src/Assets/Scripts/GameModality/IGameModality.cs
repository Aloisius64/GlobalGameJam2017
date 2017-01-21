using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModality {

    bool IsGameOver();

    GameObject GetWinnerPlayer();

    GameObject GetLoserPlayer();

}
