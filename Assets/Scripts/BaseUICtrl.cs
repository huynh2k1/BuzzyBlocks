using System.Collections.Generic;
using UnityEngine;

public class BaseUICtrl : MonoBehaviour
{
    public UIRoot[] _arrUI;
    protected Dictionary<TypeUI, UIRoot> _uis = new Dictionary<TypeUI, UIRoot>();

    protected virtual void Awake()
    {
        foreach (var ui in _arrUI)
        {
            _uis[ui.Type] = ui;
        }
    }

    public virtual void Show(TypeUI type)
    {
        if (!_uis.ContainsKey(type))
        {
            return;
        }
        _uis[type].Active();
    }

    public virtual void Hide(TypeUI type)
    {
        if (!_uis.ContainsKey(type))
        {
            return;
        }
        _uis[type].DeActive();
    }
}
