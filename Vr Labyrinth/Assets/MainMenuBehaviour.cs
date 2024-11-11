using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{

    [SerializeField] GameObject puerta;


    public void StartGame()
    {
        Debug.Log("empexzo el jeugo ");
        puerta.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
