using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorPanel : MonoBehaviour
{
    public UnityEvent<Color> onSelectColor = new UnityEvent<Color>();

    Button curButton;


    public void Activate(Button button)
    {
        curButton = button;    
    }

    public void Deactivate(Image image)
    {
        curButton.image.color = image.color;
        onSelectColor.Invoke(image.color);
    }
}