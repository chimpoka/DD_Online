using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState_TeamBuilding : GameState
{
    public GameState_TeamBuilding(Game game)
    : base(game)
    {}

    public override void update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            game.gameState = new GameState_Battle(game);
        }

        if (Input.GetMouseButtonDown(0))
            Debug.Log("Mouse");
    }
}
