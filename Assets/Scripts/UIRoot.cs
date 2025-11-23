using UnityEngine;

public abstract class UIRoot : MonoBehaviour
{
    public virtual void DeActive() => gameObject.SetActive(false);
    public virtual void Active() => gameObject.SetActive(true);
    public abstract TypeUI Type { get; }
}

public enum TypeUI
{
    HowToPlay,
    Home,
    Win,
    Lose,
    Game,
}