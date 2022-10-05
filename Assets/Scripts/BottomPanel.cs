using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BottomPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] CircleSizeButton[] buttons;
    [SerializeField] TextMeshProUGUI[] countTexts;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void ChangeColor(Color color)
    {
        foreach(var button in buttons)
        {
            button.ChangeColor(color);
        }
    }


    public void ChangeCount(int size, int count)
    {
        countTexts[size - 1].text = count.ToString();
    }
}