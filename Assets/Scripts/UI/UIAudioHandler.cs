using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _uiSound;

    public void PlaySound()
    {
        _uiSound.Play();
    }
}
