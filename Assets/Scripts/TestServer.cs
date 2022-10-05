/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestServer : MonoBehaviour
{
    bool firstPlayerTurn;
    Grid<TicTacToeElement> grid;
    GameObject[,] objs;
    int curSize;

    [SerializeField] Player player_1;
    [SerializeField] Player player_2;

    void Start()
    {
        
    }

    void PlayerTurn(Player player, GameObject figurePref, int x, int y)
    {
        Destroy(objs[x, y]);
        grid.RemoveElement(x, y);

        GameObject playersFigure = Instantiate(figurePref, grid.GetWorldPositionWithPositiveOffset(x, y), Quaternion.identity);
        playersFigure.transform.localScale *= curSize;

        TicTacToeElement ttte = new TicTacToeElement(player.id, curSize);

        switch (curSize)
        {
            case 1:
                player.Substruct(0);
                break;
            case 2:
                player.Substruct(1);
                break;
            case 3:
                player.Substruct(2);
                break;
        }


        objs[x, y] = playersFigure;
        grid.AddElement(ttte, x, y);

        EndOfTurn(player.id);
    }

    void StartPlayerTurn(Player player)
    {
        if (player.GetCountElem(0) == 0)
            smallCircleButton.interactable = false;

        if (player.GetCountElem(1) == 0)
            mediumCircleButton.interactable = false;

        if (player.GetCountElem(2) == 0)
            bigCircleButton.interactable = false;
    }

    void EndOfTurn(int idPlayer)
    {
        int numberCoincidences;


        for (int i = 0; i < gridSize.x * 2 + 2; i++)
        {
            numberCoincidences = 0;

            if (i < gridSize.x)
            {
                for (int j = 0; j < gridSize.x; j++)
                    if (grid.GetElement(i, j) != null)
                        if (grid.GetElement(i, j).idPlayer == idPlayer)
                            numberCoincidences++;
                        else
                            break;
            }
            else if (i < gridSize.x * 2)
            {
                for (int j = 0; j < gridSize.x; j++)
                    if (grid.GetElement(j, i % gridSize.x) != null)
                        if (grid.GetElement(j, i % gridSize.x).idPlayer == idPlayer)
                            numberCoincidences++;
                        else
                            break;
            }
            else if (i == gridSize.x * 2)
            {
                for (int j = 0; j < gridSize.x; j++)
                    if (grid.GetElement(j, j) != null)
                        if (grid.GetElement(j, j).idPlayer == idPlayer)
                            numberCoincidences++;
                        else
                            break;
            }
            else
                for (int j = 0, k = gridSize.x - 1; j < gridSize.x && k >= 0; j++, k--)
                    if (grid.GetElement(j, k) != null)
                        if (grid.GetElement(j, k).idPlayer == idPlayer)
                            numberCoincidences++;
                        else
                            break;

            if (numberCoincidences == 3)
            {
                GameEnd(idPlayer);
                return;
            }
        }
    }


    public void AddPlayers(Player firstPlayer, Player secondPlayer)
    {
        this.firstPlayer = firstPlayer;
        this.secondPlayer = secondPlayer;
    }

    void GameEnd(int idPlayer)
    {
        gameEndPanel.SetActive(true);

        if (firstPlayer.id == idPlayer)
            gameEndPanel.transform.GetChild(0).GetComponent<Text>().text = firstPlayer.name + " Win";
        else
            gameEndPanel.transform.GetChild(0).GetComponent<Text>().text = secondPlayer.name + " Win";
    }
}*/