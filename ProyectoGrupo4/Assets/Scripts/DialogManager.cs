using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public GameObject tutView;
    public Text dialog;

    private string sentence;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        if (tutView)
          tutView.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spawn"))
        {
            tutView.SetActive(true);
            sentence = "Welcome to this brief tutorial... Press the arrows (WASD) to move...Press spacebar to jump...";
            sentence += "Collect 4 coins to kill your enemies, else they will kill you! Don't forget to pick up the key, or you will be stuck here forever!";
            sentence += "Be patient and good luck... You'll need it.";
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spawn"))
            tutView.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spawn"))
            tutView.SetActive(false);
    }
}
