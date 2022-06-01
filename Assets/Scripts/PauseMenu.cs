using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] GameObject menu;

    public void PausetheGame()
    {
        Time.timeScale = 0.0f;
        pauseButton.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    public void ResumetheGame()
    {
        Time.timeScale = 1.0f;
        menu.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

   public void SoundMenu()
    {

    }
}
