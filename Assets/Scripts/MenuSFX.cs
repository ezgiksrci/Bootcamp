using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSFX : MonoBehaviour
{
    public AudioSource audioSource;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void MakeTheAlphaZero()
    {
        image.color = new Color (255, 255, 255, 0);
    }

    public void RevertTheAlphaValue()
    {
        image.color = new Color(255, 255, 255, 255);
    }
}
