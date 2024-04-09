using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    public Sprite[] numberSprite;
    public SpriteRenderer numberSpriteHolder;
    public SpriteRenderer rollingDiceAnimation;
    public int numberGot;

    Coroutine generateRandomNumDice;
    int OutPieces;

    PlayerPiece curentPlayerPiece;
    Coroutine MovePlayerPiece;

    public Dice diceSound;
    public PlayerSound playerSound;
    public int maxNum;
   

    public void OnMouseDown()
    {
        generateRandomNumDice = StartCoroutine(RollingDiceClicked());
    }

    IEnumerator RollingDiceClicked()
    {
        yield return new WaitForEndOfFrame(); // wait for end of the frame

        if (GameManager.gm.canDiceRoll)  // play the sound whene dice roll
        {
            if (GameManager.gm.sound)
            {
                diceSound.PlaySound();
            }
        GameManager.gm.canDiceRoll = false;  // dice can not be touched during dice rolling
        numberSpriteHolder.gameObject.SetActive(false); // sprite number set active false
        rollingDiceAnimation.gameObject.SetActive(true); // roling dice animation play
        yield return new WaitForSeconds(0.6f); // after dice animation play 6 seconds do something for show number sprite

        if(GameManager.gm.totalSix == 2) { GameManager.gm.totalSix = 0; maxNum = 5; } else { maxNum = 6; }
        numberGot = Random.Range(0, maxNum);

        numberSpriteHolder.sprite = numberSprite[numberGot]; // 
        numberGot += 1;
        GameManager.gm.numberOfStepsToMove = numberGot; // number 
        GameManager.gm.rollingDice = this;
        if(numberGot == 6) { GameManager.gm.totalSix += 1; } // total num of six counts 

        numberSpriteHolder.gameObject.SetActive(true);  // number sprite  set activated
        rollingDiceAnimation.gameObject.SetActive(false); // rolling dice animtation stop activating

        yield return new WaitForEndOfFrame();
            //     canDiceRoll = true;

            if(GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0]) {OutPieces = GameManager.gm.RedOutPlayer; curentPlayerPiece = GameManager.gm.managePlayerPieces[0]; }
            else if(GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[1]) {OutPieces = GameManager.gm.BlueOutPlayer; curentPlayerPiece = GameManager.gm.managePlayerPieces[1]; }
            else if(GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2]) {OutPieces = GameManager.gm.GreenOutPlayer; curentPlayerPiece = GameManager.gm.managePlayerPieces[2]; }
            else if(GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[3]) { OutPieces = GameManager.gm.YellowOutPlayer; curentPlayerPiece = GameManager.gm.managePlayerPieces[3]; }

            if (GameManager.gm.numberOfStepsToMove != 6 && OutPieces == 0)
            {
                GameManager.gm.canDiceRoll = true;
                GameManager.gm.selfDice= false;
                GameManager.gm.transferDice = true;

                yield return new WaitForSeconds(0.6f);
                GameManager.gm.RollingDiceManager();
            }
            else
            {
                if (GameManager.gm.numberOfStepsToMove == 6 && OutPieces == 0)
                {
                    MakePlayerReadyToMove();
                }
                else if (OutPieces > 0)
                {
                    MovePlayerPiece = StartCoroutine(moveSteps_Enum());
                }
            }

        if (generateRandomNumDice != null)
        {
            StopCoroutine(RollingDiceClicked());
        }
     }

    }
    public void MakePlayerReadyToMove()
    {
        curentPlayerPiece.isReady = true;
        curentPlayerPiece.transform.position = curentPlayerPiece.pathParent.commonPathPoint[0].transform.position;
        curentPlayerPiece.numberOfStepsAlreadyMoved = 1;

        curentPlayerPiece.previousPathPoint = curentPlayerPiece.pathParent.commonPathPoint[0];
        curentPlayerPiece.currnetPathPoint = curentPlayerPiece.pathParent.commonPathPoint[0];
        GameManager.gm.AddPathPoint(curentPlayerPiece.currnetPathPoint);
        // add player piece in current pathpoint 
        curentPlayerPiece.currnetPathPoint.AddPlyerPiece(curentPlayerPiece);
        // if 6 not appear in rolling dice  then dice transfer automatically
        GameManager.gm.canDiceRoll = true; // dice can be touched after transfer
        GameManager.gm.transferDice = false; // dice can not be transfer
        GameManager.gm.selfDice = true;   // self dice true if six appears
        GameManager.gm.RollingDiceManager(); //

        GameManager.gm.numberOfStepsToMove = 0; // after player reach the destination number make this variable zero that player can not move forward
        outPlayer();  // if player pice out of home then make a variable 1
    }
    public void outPlayer()
    {
        if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0]) { GameManager.gm.RedOutPlayer += 1; }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[1]) { GameManager.gm.BlueOutPlayer += 1; }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2]) { GameManager.gm.GreenOutPlayer += 1; }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[3]) { GameManager.gm.YellowOutPlayer += 1; }
    
    }

    IEnumerator moveSteps_Enum()
    {
        curentPlayerPiece.numberOfStepsToMove = GameManager.gm.numberOfStepsToMove;
        for (int i = curentPlayerPiece.numberOfStepsAlreadyMoved; i < (curentPlayerPiece.numberOfStepsAlreadyMoved + curentPlayerPiece.numberOfStepsToMove); i++)
        {
            if (isPathAvaialableToMove(curentPlayerPiece.numberOfStepsToMove, curentPlayerPiece.numberOfStepsAlreadyMoved))
            {
                curentPlayerPiece.transform.position = curentPlayerPiece.pathParent.commonPathPoint[i].transform.position;
                if (GameManager.gm.sound)
                {
                    playerSound.PlaySound();
                }
                yield return new WaitForSeconds(0.3f);
            }
        }
        if (isPathAvaialableToMove(curentPlayerPiece.numberOfStepsToMove, curentPlayerPiece.numberOfStepsAlreadyMoved))
        {
            curentPlayerPiece.numberOfStepsAlreadyMoved += curentPlayerPiece.numberOfStepsToMove;

            GameManager.gm.RemovePathPoint(curentPlayerPiece.previousPathPoint);
            curentPlayerPiece.currnetPathPoint.RemovePlayerPiece(curentPlayerPiece);
            curentPlayerPiece.currnetPathPoint = curentPlayerPiece.pathParent.commonPathPoint[curentPlayerPiece.numberOfStepsAlreadyMoved - 1];

            GameManager.gm.AddPathPoint(curentPlayerPiece.currnetPathPoint);
            curentPlayerPiece.previousPathPoint = curentPlayerPiece.currnetPathPoint;

            if (GameManager.gm.numberOfStepsToMove != 6)
            {
                GameManager.gm.selfDice = false;
                GameManager.gm.transferDice = true;
            }
            else
            {
                GameManager.gm.selfDice = true;
                GameManager.gm.transferDice = false;

            }
            SnakeLadderPoint();
            GameManager.gm.numberOfStepsToMove = 0;
        }
        GameManager.gm.RollingDiceManager();

        if (curentPlayerPiece.numberOfStepsAlreadyMoved == 100)// if player piece reach 100 path points then make variable 1
        {
            string playerPieceName = curentPlayerPiece.name;
            if (playerPieceName == "RedPlayerPiece") { GameManager.gm.RedCompletedPlayer += 1; } // red variable becomes one
            else if (playerPieceName == "BluePlayerPiece") { GameManager.gm.BlueCompletedPlayer += 1; }//
            else if (playerPieceName == "GreenPlayerPiece") { GameManager.gm.GreenCompletedPlayer += 1; }
            else if (playerPieceName == "YellowPlayerPiece") { GameManager.gm.YellowCompletedPlayer += 1;  }
        }

        yield return new WaitForSeconds(.3f);
        GameManager.gm.RollingDiceManager();

        CompleteGame(); // 

        if (MovePlayerPiece != null)
        {
            StopCoroutine(moveSteps_Enum());
        }
    }

    public void CompleteGame()
    {
        int fvalue = 3;
        int totalCompletePlayer = GameManager.gm.RedCompletedPlayer + GameManager.gm.BlueCompletedPlayer + GameManager.gm.GreenCompletedPlayer + GameManager.gm.YellowCompletedPlayer;
        if(GameManager.gm.totalPlayerCanPlay == 4) { fvalue = 3; }
        if (GameManager.gm.totalPlayerCanPlay == 2) { fvalue = 1; }
        if (GameManager.gm.totalPlayerCanPlay == 3) { fvalue = 2; }
      
        
        if (totalCompletePlayer == fvalue)
        {
            GameManager.gm.GamePanel.SetActive(false);
            GameManager.gm.completeGame.SetActive(true);
        }
    }

    bool isPathAvaialableToMove(int numOfStepsToMove, int numOfStepsAlreadyMove)
    {
        if (curentPlayerPiece.numberOfStepsToMove == 0)
        {
            return false;
        }
        int leftNumPath = 100 - numOfStepsAlreadyMove;
        if (leftNumPath >= numOfStepsToMove)
        {

            return true;
        }
        else
        {

            return false;
        }

    }
    public void SnakeLadderPoint()
    {
        int[] SnakeLadderEnterPoints = { 2, 6, 20, 52, 57, 52, 71, 50, 43, 99,74, 87, 84 };
        int[] SnakeLadderOuterPoints = { 23, 45, 59, 72, 96, 72, 92, 5, 17, 40,15, 49, 58 };

        if (SnakeLadderEnterPoints.Contains(curentPlayerPiece.numberOfStepsAlreadyMoved)) 
        { 
        int Index = System.Array.IndexOf(SnakeLadderEnterPoints, curentPlayerPiece.numberOfStepsAlreadyMoved);
        curentPlayerPiece.numberOfStepsAlreadyMoved = SnakeLadderOuterPoints[Index];
        curentPlayerPiece.transform.position = curentPlayerPiece.pathParent.commonPathPoint[curentPlayerPiece.numberOfStepsAlreadyMoved - 1].transform.position;
        }

    }
}
