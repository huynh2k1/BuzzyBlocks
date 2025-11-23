using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MusicCtrl : MonoBehaviour
{
    public static MusicCtrl I;

    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioClip _music;

    [SerializeField] AudioSource[] _soundSource;
    Queue<AudioSource> _qSources;

    [SerializeField] AudioClip _click, _win, _lose, _stack, _placed, _move;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        _qSources = new Queue<AudioSource>(_soundSource);
        PlayMusic();
    }

    public void PlaySFXByType(TypeSFX type)
    {
        switch (type)
        {
            case TypeSFX.CLICK:
                PlaySFX(_click);
                break;
            case TypeSFX.PLACED:
                PlaySFX(_placed);
                break;
            case TypeSFX.WIN:
                PlaySFX(_win);
                break;
            case TypeSFX.LOSE:
                PlaySFX(_lose);
                break;
            case TypeSFX.STACK:
                PlaySFX(_stack);    
                break;
            case TypeSFX.MOVE:
                PlaySFX(_move);
                break;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        var source = _qSources.Dequeue();
        if (source == null)
            return;

        source.PlayOneShot(clip);
        _qSources.Enqueue(source);
    }

    public void PlayMusic(float volume = 0.7f)
    {
        _musicSource.clip = _music;
        _musicSource.Play();
    }

}
public enum TypeSFX
{
    MUSIC,
    CLICK,
    WIN,
    LOSE,
    STACK,
    PLACED,
    MOVE,
}


