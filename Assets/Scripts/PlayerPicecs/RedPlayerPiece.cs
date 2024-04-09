using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayerPiece : PlayerPiece
{
    public void OnMouseDown()
    {
        if (GameManager.gm.rollingDice != null) 
        { 
        if (!isReady)
        {
                if (GameManager.gm.rollingDice == Dice && GameManager.gm.numberOfStepsToMove == 6)
                {
                    GameManager.gm.RedOutPlayer += 1;
                    MakePlayerReadyToMove();
                    GameManager.gm.numberOfStepsToMove = 0;
                    return;
                }
        }
            if (GameManager.gm.rollingDice == Dice && isReady)
            {
                MoveSteps();
            }
        }
    }

}