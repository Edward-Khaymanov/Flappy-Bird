using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIMover))]
[RequireComponent(typeof(TextAnimator))]
public class ScoreBoardView : MonoBehaviour
{
    [SerializeField] private Image _coinTemplate;
    [SerializeField] private TMP_Text _bestScore;
    [SerializeField] private ParticleSystem _coinSparkle;

    private UIMover _UIMover;
    private TextAnimator _textAnimator;

    private void Awake()
    {
        _UIMover = GetComponent<UIMover>();
        _textAnimator = GetComponent<TextAnimator>();
    }

    public IEnumerator Render(ScoreBoard scoreBoard)
    {
        _bestScore.text = $"{scoreBoard.Highscore}";

        yield return StartCoroutine(_UIMover.Move());
        yield return StartCoroutine(_textAnimator.Count(0, scoreBoard.CurrentScore));

        SetCoinSprite(scoreBoard.CoinSprite);

        if (_coinTemplate.sprite != null)
            PlaySparkles();
    }

    public void ResetView()
    {
        _UIMover.ResetPosition();
        _textAnimator.ResetText();
        _coinTemplate.sprite = null;
        _coinTemplate.color = new Color(1, 1, 1, 0);
        StopSparkles();
    }

    private void SetCoinSprite(Sprite coin)
    {
        _coinTemplate.sprite = coin;

        var alpha = coin == null ? 0 : 1;
        _coinTemplate.color = new Color(1, 1, 1, alpha);
    }

    private void PlaySparkles()
    {
        _coinSparkle.Play();
    }

    private void StopSparkles()
    {
        _coinSparkle.Stop();
    }
}
