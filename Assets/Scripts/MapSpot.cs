using System;
using UnityEngine;
using UnityEngine.UI;

public class MapSpot : MonoBehaviour
{
    public static event Action<MapSpot> Activated;

    public int SceneIndex => _targetSceneIndex;

    [SerializeField] private int _targetSceneIndex;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _inactiveSprite;

    public void Activate()
    {
        Activated?.Invoke(this);
    }

    public void Disable()
    {
        _image.sprite = _inactiveSprite;
        _button.enabled = false;
    }
}
