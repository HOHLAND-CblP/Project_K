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

        
        transform.localPosition = new Vector3(150, button.transform.localPosition.y);
        
    }

    public void Deactivate(Image image)
    {
        curButton.image.color = image.color;
        onSelectColor.Invoke(image.color);
    }
}