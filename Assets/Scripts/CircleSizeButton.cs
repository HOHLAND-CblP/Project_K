using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSizeButton : MonoBehaviour
{
    [SerializeField] Image circleImage;
    [SerializeField] GameObject backout;


    public void ChangeColor(Color color)
    {
        circleImage.color = color;
    }

    public void DeactivateButton()
    {
        GetComponent<Button>().interactable = false;
        backout.SetActive(true);
    }
}
