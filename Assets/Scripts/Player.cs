using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int id;
    public string name;
    public Color32 color;
    public Sprite figureSprite;

    int count;
    private int[] countElem = new int[3];


    public Player(int id, int count)
    {
        this.id = id;
        name = "";
        color = Color.white;

        this.count = count;
        countElem = new int[3];
        for (int i = 0; i < 3; i++)
            countElem[i] = count;
    }


    public Player(int id, string name, int count)
    {
        this.id = id;
        this.name = name;
        color = Color.white;

        countElem = new int[3];
        for (int i = 0; i < 3; i++)
            countElem[i] = count;
    }


    public void Reload()
    {
        for (int i = 0; i < 3; i++)
            countElem[i] = count;
    }


    public int GetCountElem(int index)
    {
        if (index >= 0 && index < 3)
            return countElem[index];
        return 0;
    }

    public void Substruct(int index)
    {
        if (index >= 0 && index < 3)
            countElem[index]--;
    }
}