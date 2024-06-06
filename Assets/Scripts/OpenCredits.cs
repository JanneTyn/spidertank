using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCredits : MonoBehaviour
{
    private Transform startGame;
    private Transform options;
    private Transform credits;
    private Transform quitGame;
    private Transform logo;
    private Transform creditsPage;

    // Start is called before the first frame update

    public void CreditsPageOpen()
    {
        startGame = transform.Find("Canvas/Start Game");
        options = transform.Find("Canvas/Options");
        credits = transform.Find("Canvas/Credits");
        quitGame = transform.Find("Canvas/Quit Game");
        logo = transform.Find("Canvas/Logo");
        creditsPage = transform.Find("Canvas/CreditsPage");

        startGame.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);
        logo.gameObject.SetActive(false);
        creditsPage.gameObject.SetActive(true);
    }

    public void CreditsPageClose()
    {
        startGame = transform.Find("Canvas/Start Game");
        options = transform.Find("Canvas/Options");
        credits = transform.Find("Canvas/Credits");
        quitGame = transform.Find("Canvas/Quit Game");
        logo = transform.Find("Canvas/Logo");
        creditsPage = transform.Find("Canvas/CreditsPage");

        startGame.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
        quitGame.gameObject.SetActive(true);
        logo.gameObject.SetActive(true);
        creditsPage.gameObject.SetActive(false);
    }
}
