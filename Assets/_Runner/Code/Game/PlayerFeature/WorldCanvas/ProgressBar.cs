using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.PlayerFeature.WorldCanvas
{
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _viewProgressBar;
    [SerializeField] private Image _colorProgressBar;
    [SerializeField] private float _appearDuration = 0.3f;

    private Tween _fadeTween;

    public void SetActive(bool value)
    {
        var tartAlpha = value == true ? 1 : 0;
        KillTween();
        _fadeTween = _canvasGroup.DOFade(tartAlpha, _appearDuration);
    }

    public void SetProgressBar(int current, int max, Color progressBarColor)
    {
        DOTween.To(() => _viewProgressBar.fillAmount, x => _viewProgressBar.fillAmount = x, current / (float) max,
                   0.3f);
        DOTween.To(() => _colorProgressBar.color, x => _colorProgressBar.color = x, progressBarColor, 0.3f);
    }

    private void KillTween() => _fadeTween?.Kill();

    private void OnDestroy() => KillTween();
}
}