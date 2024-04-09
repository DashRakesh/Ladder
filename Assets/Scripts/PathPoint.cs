using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public PathObjectParent pathObjectParent;
    public List<PlayerPiece> playerPieces = new List<PlayerPiece>();

    // Start is called before the first frame update
    void Start()
    {
        pathObjectParent = GetComponent<PathObjectParent>();
    }
    public void AddPlyerPiece(PlayerPiece pathPoint)
    {
        if (playerPieces.Count > 0)
        {
            KilPlayer();
        }
        playerPieces.Add(pathPoint);
     //  RescaleAndRepositionAllPlayerPieces();
    }

    public void KilPlayer()
    {
        if (playerPieces[0].numberOfStepsAlreadyMoved < 100) 
        { 
        float xposition = 0;
        float yposition = 0;
        playerPieces[0].isReady = false;
        playerPieces[0].numberOfStepsAlreadyMoved = 0;
        string playerPieceName = playerPieces[0].name;
        if (playerPieceName == "RedPlayerPiece") { GameManager.gm.RedOutPlayer = 0; xposition = -0.324f; yposition = -3.602f; }
        else if (playerPieceName == "BluePlayerPiece") { GameManager.gm.BlueOutPlayer = 0; xposition = 0.357f; yposition = -3.633f; }
        else if (playerPieceName == "GreenPlayerPiece") { GameManager.gm.GreenOutPlayer = 0; xposition = 0.46f; yposition = 3.6f; }
        else if (playerPieceName == "YellowPlayerPiece") { GameManager.gm.YellowOutPlayer = 0; xposition = -0.38f; yposition = 3.6f; }
        playerPieces[0].transform.position = new Vector3(xposition, yposition, 0);
        RemovePlayerPiece(playerPieces[0]);
        }
    }

    public void RemovePlayerPiece(PlayerPiece pathPoint)
    {
        if (playerPieces.Contains(pathPoint))
        {
            playerPieces.Remove(pathPoint);
      //     RescaleAndRepositionAllPlayerPieces();
        }
    }
    public void RescaleAndRepositionAllPlayerPieces()
    {
        int plsCount = playerPieces.Count;
        bool isOdd = (plsCount % 2) == 0 ? false : true;
        int spriteLayer = 0;

        int extent = plsCount / 2;
        int counter = 0;

        if (isOdd)
        {
            for (int i = -extent; i <= extent; i++)
            {
       //        playerPieces[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount - 1], 1f);
                playerPieces[counter].transform.position =  new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;
            }
        }
        else
        {
            for (int i = -extent; i < extent; i++)
            {
      //         playerPieces[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount - 1], 1f);
                playerPieces[counter].transform.position =new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;
            }

        }
        for (int i = 0; i < playerPieces.Count; i++)
        {
            playerPieces[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spriteLayer;
            spriteLayer++;
        }
    }
}
