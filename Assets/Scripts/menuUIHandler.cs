using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuUIHandler : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
         audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PlayTheGame()
    {
        SceneManager.LoadScene(1);
    }
}
