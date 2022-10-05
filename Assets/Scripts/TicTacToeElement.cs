using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeElement
{
    public int idPlayer;
    public int size;

    public TicTacToeElement(int idPlayer, int size)
    {
        this.idPlayer = idPlayer;
        this.size = size;
    }
}