using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : Screen
{
    public GameController gameController;

    public void NextLevel()
    {
        gameController.ChangeState(GameController.gameStates.game);    
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
