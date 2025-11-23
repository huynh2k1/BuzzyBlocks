using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonJelly : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Button _btn;

    [Header("Jelly Settings")]
    public float pressedScale = 0.85f;
    public float pressDuration = 0.12f;
    public float releaseDuration = 0.5f;

    [Header("Anti Spam")]
    public float clickDelay = 0.2f;
    bool canClick = true;

    RectTransform rect;
    Vector3 originalScale;
    Tween tween;
    Tween delayTween;   // <-- tween thay cho coroutine

    void Awake()
    {
        _btn = GetComponent<Button>();
        rect = GetComponent<RectTransform>();
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canClick) return;

        tween?.Kill();

        transform.DOScale(originalScale * pressedScale, pressDuration)
            .SetEase(Ease.OutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canClick) return;

        tween?.Kill();

        tween = rect.DOScale(originalScale, releaseDuration)
            .SetEase(Ease.OutElastic, 1.2f, 0.5f);

        if (_btn && _btn.interactable)
        {
            _btn.onClick.Invoke();
            MusicCtrl.I.PlaySFXByType(TypeSFX.CLICK);
        }

        StartDelay();
    }

    void StartDelay()
    {
        canClick = false;
        if (_btn) _btn.interactable = false;

        // ❗ Thay coroutine bằng DOTween delay
        delayTween?.Kill();
        delayTween = DOVirtual.DelayedCall(clickDelay, () =>
        {
            canClick = true;
            if (_btn) _btn.interactable = true;
        }).SetAutoKill(true);
    }

}
