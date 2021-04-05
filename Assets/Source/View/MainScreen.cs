using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : Screen
{
    public GameController gameController;

    public override void Initialize()
    {
        base.Initialize();
    }

    public void OnPlayButtonClicked()
    {
        // change state in game controller
        gameController.ChangeState(GameController.gameStates.game);
    }

}
