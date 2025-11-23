using DG.Tweening;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Renderer _meshRenderer;
    [SerializeField] Collider _collider;

    public HexaStack HexStack { get; private set; }
    public Color Color
    {
        get => _meshRenderer.material.color;
        set => _meshRenderer.material.color = value;
    }

    //Set hexa hiện tại cho 1 cái stack chứa nó
    public void Configure(HexaStack hexStack)
    {
        HexStack = hexStack;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
    //Di chuyển hexagon sang stack khác
    public void MoveToLocal(Vector3 targetPos)
    {
        LeanTween.cancel(gameObject);
        transform.DOKill();
        float delay = transform.GetSiblingIndex() * 0.05f;
        transform.DOLocalMove(targetPos, 0.3f)
            .SetEase(Ease.Linear)
            .SetDelay(delay);

        Vector3 dir = (targetPos - transform.localPosition).With(y: 0).normalized;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, dir);
        MusicCtrl.I.PlaySFXByType(TypeSFX.MOVE);
        LeanTween.rotateAround(gameObject, rotationAxis, 180, 0.3f)
            .setEase(LeanTweenType.easeInOutSine).setDelay(delay);
        //transform.DOLocalRotate(new Vector3(180, 0, 0), 0.2f)
        //    .SetEase(Ease.InOutSine).SetDelay(delay);
    }

    //Biến mất
    public void Vanish(float delay)
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, 0.3f)
            .SetDelay(delay)
            .SetEase(Ease.InOutSine).OnComplete(() =>
            {
                MusicCtrl.I.PlaySFXByType(TypeSFX.STACK);
                Destroy(gameObject);
            });
    }
}
