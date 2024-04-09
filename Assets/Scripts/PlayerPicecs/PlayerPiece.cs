using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public bool isReady;
    public bool moveNow;
    public int numberOfStepsToMove;
    public int numberOfStepsAlreadyMoved;

    public PathObjectParent pathParent;
    public RollingDice Dice;
    Coroutine MovePlayerPiece;

    public PathPoint previousPathPoint;
    public PathPoint currnetPathPoint;

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }
    public void MakePlayerReadyToMove()
    {
        isReady = true;
        transform.position = pathParent.commonPathPoint[0].transform.position;
        numberOfStepsAlreadyMoved = 1;

        previousPathPoint = pathParent.commonPathPoint[0];
        currnetPathPoint = pathParent.commonPathPoint[0];
        GameManager.gm.AddPathPoint(currnetPathPoint);
        currnetPathPoint.AddPlyerPiece(this);

        GameManager.gm.canDiceRoll = true;
        GameManager.gm.transferDice = false;
        GameManager.gm.selfDice = true;

        GameManager.gm.RollingDiceManager();
}
    public void MoveSteps()
    {
         
    }
    IEnumerator moveSteps_Enum()
    {
        numberOfStepsToMove = GameManager.gm.numberOfStepsToMove;
        for (int i = numberOfStepsAlreadyMoved; i < (numberOfStepsAlreadyMoved + numberOfStepsToMove); i++)
        {
            if (isPathAvaialableToMove(numberOfStepsToMove, numberOfStepsAlreadyMoved)) 
            { 
            transform.position = pathParent.commonPathPoint[i].transform.position;
            yield return new WaitForSeconds(0.3f);
            }
        }
        if (isPathAvaialableToMove(numberOfStepsToMove, numberOfStepsAlreadyMoved))
        {
            numberOfStepsAlreadyMoved += numberOfStepsToMove;

            GameManager.gm.RemovePathPoint(previousPathPoint);
            currnetPathPoint.RemovePlayerPiece(this);
            currnetPathPoint = pathParent.commonPathPoint[numberOfStepsAlreadyMoved - 1];

            GameManager.gm.AddPathPoint(currnetPathPoint);
            previousPathPoint = currnetPathPoint;

            if(GameManager.gm.numberOfStepsToMove != 6)
            {
                GameManager.gm.selfDice = false;
                GameManager.gm.transferDice= true;
            }
            else
            {
                GameManager.gm.selfDice = true;
                GameManager.gm.transferDice = false;

            }
            GameManager.gm.numberOfStepsToMove = 0;
        }
        GameManager.gm.RollingDiceManager();

        if (MovePlayerPiece != null)
        {
            StopCoroutine(moveSteps_Enum());
        }
    }
    bool isPathAvaialableToMove(int numOfStepsToMove,int numOfStepsAlreadyMove)
    {
        if(numberOfStepsToMove == 0)
        {
            return false;
        }
        int leftNumPath = 100 - numOfStepsAlreadyMove;
        if(leftNumPath >= numOfStepsToMove)
        {
            return true;
        }
        else
        {
            return false;
        }
  
    }

}
