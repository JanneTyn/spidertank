using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptions : MonoBehaviour
{
    private Transform startGame;
    private Transform options;
    private Transform credits;
    private Transform quitGame;
    private Transform logo;
    private Transform optPage;

    // Start is called before the first frame update

    public void OptPageOpen()
    {
        startGame = transform.Find("Canvas/Start Game");
        options = transform.Find("Canvas/Options");
        credits = transform.Find("Canvas/Credits");
        quitGame = transform.Find("Canvas/Quit Game");
        logo = transform.Find("Canvas/Logo");
        optPage = transform.Find("Canvas/OptionsPage");

        startGame.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);
        logo.gameObject.SetActive(false);
        optPage.gameObject.SetActive(true);
    }

    public void OptPageClose()
    {
        startGame = transform.Find("Canvas/Start Game");
        options = transform.Find("Canvas/Options");
        credits = transform.Find("Canvas/Credits");
        quitGame = transform.Find("Canvas/Quit Game");
        logo = transform.Find("Canvas/Logo");
        optPage = transform.Find("Canvas/OptionsPage");

        startGame.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
        quitGame.gameObject.SetActive(true);
        logo.gameObject.SetActive(true);
        optPage.gameObject.SetActive(false);
    }
}
