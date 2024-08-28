

using System;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    MoveScene moveScene;
    private void Awake()
    {
        moveScene = FindAnyObjectByType<MoveScene>();
    }

    public void MoveToMode(string gameName)
    {
        GameModeData.gameName = (GameName)Enum.Parse(typeof(GameName), gameName);
        moveScene.MoveToScene("LevelScene");
    }
}