using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveText : MonoBehaviour
{

    [SerializeField] private Canvas textCanvas;
    [SerializeField] private Text displayText;

    [SerializeField]
    [TextArea]
    private string message = "Default message";
    
    // Start is called before the first frame update
    void Start()
    {
     if(textCanvas != null)
        {
            textCanvas.gameObject.SetActive(false);
        }   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ShowText());
          //  gameObject.SetActive(false);

        }
    }

    private IEnumerator ShowText()
    {
    if(textCanvas != null && displayText != null)
        {
            textCanvas.gameObject.SetActive(true); 
            displayText.text = message;

            yield return new WaitForSeconds(3);

            textCanvas.gameObject.SetActive(false);
        }

    }

}
