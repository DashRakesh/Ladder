using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject mainPanel;

    public PlayerPiece redPlayerPiece;
    public PlayerPiece bluePlayerPiece;
    public PlayerPiece greenPlayerPiece;
    public PlayerPiece yellowPlayerPiece;

    public GameObject redRollingPlace;
    public GameObject blueRollingPlace;
    public GameObject greenRollingPlace;
    public GameObject yellowRollingPlace;


    public void Game1()
    {
        gamePanel.SetActive(true);
        mainPanel.SetActive(false);
        GameManager.gm.totalPlayerCanPlay = 1;
        bluePlayerPiece.gameObject.SetActive(false);
        yellowPlayerPiece.gameObject.SetActive(false);
        blueRollingPlace.SetActive(false);
        yellowRollingPlace.SetActive(false);

    }
    public void Game2()
    {
        gamePanel.SetActive(true);
        mainPanel.SetActive(false);
        GameManager.gm.totalPlayerCanPlay = 2;
        bluePlayerPiece.gameObject.SetActive(false);
        yellowPlayerPiece.gameObject.SetActive(false);
        blueRollingPlace.SetActive(false);
        yellowRollingPlace.SetActive(false);

    }
    public void Game3()
    {
        gamePanel.SetActive(true);
        mainPanel.SetActive(false);
        GameManager.gm.totalPlayerCanPlay = 3;
        yellowPlayerPiece.gameObject.SetActive(false);
        yellowRollingPlace.SetActive(false);
    }
    public void Game4()
    {
        gamePanel.SetActive(true);
        mainPanel.SetActive(false);
        GameManager.gm.totalPlayerCanPlay = 4;
    }
}
