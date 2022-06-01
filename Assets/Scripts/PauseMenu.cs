using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public void PausetheGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumetheGame()
    {
        Time.timeScale = 1.0f;
    }
}
