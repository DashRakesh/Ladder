using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public RollingDice rollingDice;
    public int numberOfStepsToMove;
 

    List<PathPoint> playerOnPathPointList = new List<PathPoint>();

    public bool canDiceRoll = true;
    public bool transferDice = false;
    public bool selfDice = true;

    public RollingDice[] manageRollingDice;
    public PlayerPiece[] managePlayerPieces;

    public int BlueOutPlayer = 0;
    public int GreenOutPlayer = 0;
    public int RedOutPlayer = 0;
    public int YellowOutPlayer = 0;

    public int BlueCompletedPlayer = 0;
    public int GreenCompletedPlayer = 0;
    public int RedCompletedPlayer = 0;
    public int YellowCompletedPlayer = 0;

    public bool sound = true;
    public int totalSix;

    public GameObject GamePanel;
    public GameObject completeGame;

    public int totalPlayerCanPlay;

    private void Awake()
    {
        gm = this;
    }
    public void AddPathPoint(PathPoint pathPoint)
    {
        playerOnPathPointList.Add(pathPoint);
    }
    public void RemovePathPoint(PathPoint pathPoint)
    {
        if (playerOnPathPointList.Contains(pathPoint))
        {
            playerOnPathPointList.Remove(pathPoint);
        }
    }
    public void RollingDiceManager()
    {
        int nextDice;
        if (GameManager.gm.transferDice)
        {
            if (GameManager.gm.totalPlayerCanPlay == 1)
            {
                if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0])
                {
                    GameManager.gm.manageRollingDice[0].gameObject.SetActive(false);
                    GameManager.gm.manageRollingDice[2].gameObject.SetActive(true);
                    GameManager.gm.manageRollingDice[2].OnMouseDown();
                }
                else
                {
                    GameManager.gm.manageRollingDice[0].gameObject.SetActive(true);
                    GameManager.gm.manageRollingDice[2].gameObject.SetActive(false);
                }

            }
          else  if (GameManager.gm.totalPlayerCanPlay == 2)
            {
                if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2])
                {
                    GameManager.gm.manageRollingDice[0].gameObject.SetActive(true);
                    GameManager.gm.manageRollingDice[2].gameObject.SetActive(false);
                }
                else
                {
                    GameManager.gm.manageRollingDice[0].gameObject.SetActive(false);
                    GameManager.gm.manageRollingDice[2].gameObject.SetActive(true);
                }

            }
            else if (GameManager.gm.totalPlayerCanPlay == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    //    if(i == 2) { nextDice = 0; } else{ nextDice = i + 1; }
                    nextDice = threePlayerPassout(i);
                    if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[i])
                    {
                        GameManager.gm.manageRollingDice[i].gameObject.SetActive(false);
                        GameManager.gm.manageRollingDice[nextDice].gameObject.SetActive(true);
                        break;
                    }
                }

            }
          else
            {
                for (int i = 0; i < 4; i++)
                {
                    //    if(i == 3) { nextDice = 0; } else{ nextDice = i + 1; }
                    nextDice = passout(i);
                    if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[i])
                    {
                        GameManager.gm.manageRollingDice[i].gameObject.SetActive(false);
                        GameManager.gm.manageRollingDice[nextDice].gameObject.SetActive(true);
                        break;
                    }
                }
            }


            GameManager.gm.canDiceRoll = true;
        }
        else
        {
            if (GameManager.gm.selfDice)
            {
                GameManager.gm.canDiceRoll = true;
                GameManager.gm.selfDice = false;
                if(GameManager.gm.totalPlayerCanPlay == 1 && GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2])
                {
                    GameManager.gm.manageRollingDice[2].OnMouseDown();
                }
            }

        }
    }
    int passout(int i)
    {
        /* if (i == 0) { if(GameManager.gm.RedCompletedPlayer > 0) { return (i + 1); } }
         else if( i == 1) { if (GameManager.gm.BlueCompletedPlayer > 0) { return (i + 1); } }
         else if( i == 2) { if (GameManager.gm.GreenCompletedPlayer > 0) { return (i + 1); } }
         else if( i == 3) { if (GameManager.gm.YellowCompletedPlayer > 0) { return 0; } }*/

        if (i == 3) { i = 0; } else { i += 1; }
        if (i == 0) { if( GameManager.gm.RedCompletedPlayer > 0 ) { ++i; } }
        if (i == 1) { if( GameManager.gm.BlueCompletedPlayer > 0 ) { ++i; } }
        if (i == 2) { if( GameManager.gm.GreenCompletedPlayer > 0 ) { ++i; } }
        if(i == 3)
        {
            if (GameManager.gm.YellowCompletedPlayer > 0) {
                i = 0;
                if (i == 0) { if (GameManager.gm.RedCompletedPlayer > 0) { ++i; } }
                if (i == 1) { if (GameManager.gm.BlueCompletedPlayer > 0) { ++i; } }
                if (i == 2) { if (GameManager.gm.GreenCompletedPlayer > 0) { ++i; } }
            }
        }
       return i;
    }

    int threePlayerPassout(int i)
    {

        if (i == 2) { i = 0; } else { i += 1; }
        if (i == 0) { if (GameManager.gm.RedCompletedPlayer > 0) { ++i; } }
        if (i == 1) { if (GameManager.gm.BlueCompletedPlayer > 0) { ++i; } }
        if (i == 2)
        {
            if (GameManager.gm.YellowCompletedPlayer > 0)
            {
                i = 0;
                if (i == 0) { if (GameManager.gm.RedCompletedPlayer > 0) { ++i; } }
                if (i == 1) { if (GameManager.gm.BlueCompletedPlayer > 0) { ++i; } }
            }
        }
        return i;
    }

}
