using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _getPointSound;

    private void OnEnable()
    {
        EventsHolder.PassedScoreZone += OnPassedScoreZone;
    }

    private void OnDisable()
    {
        EventsHolder.PassedScoreZone -= OnPassedScoreZone;
    }

    private void OnPassedScoreZone()
    {
        _getPointSound.Play();
    }
}
