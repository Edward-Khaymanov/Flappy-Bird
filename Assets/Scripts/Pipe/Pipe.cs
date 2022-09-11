using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _scoreZoneCollider;

    public BoxCollider2D ScoreZoneCollider => _scoreZoneCollider;
}
