using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    [Header("Parametrs")]
    public float outputDelay;
    TextMeshProUGUI text;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void NewNotification(string textNotice)
    {
        gameObject.SetActive(true);
        StopAllCoroutines();

        StartCoroutine(OutputNotice(textNotice));
    }


    IEnumerator OutputNotice(string textNotice)
    {
        text.text = "";

        foreach (char letter in textNotice)
        {
            text.text += letter;
            yield return new WaitForSeconds(outputDelay);
        }

        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}