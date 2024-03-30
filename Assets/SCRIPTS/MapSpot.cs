using System;
using UnityEngine;
using UnityEngine.UI;

public class MapSpot : MonoBehaviour
{
    public static event Action<MapSpot> Clicked;

    private string _currentPrefsName;

    private const string isUsedPrefsName = "Used";

    public int SceneIndex => _targetSceneIndex;

    [SerializeField] private int _targetSceneIndex;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _inactiveSprite;

    public void Initialize(int index)
    {
        PlayerPrefs.DeleteAll();
        var used = false;
        _currentPrefsName = isUsedPrefsName + index.ToString();
        if (PlayerPrefs.HasKey(_currentPrefsName))
        {
            used = PlayerPrefs.GetInt(_currentPrefsName) == 1;
        }
        if(used)
        {
            _image.sprite = _inactiveSprite;
            _button.enabled = false;
        }
    }

    public void Click()
    {
        Clicked?.Invoke(this);
    }

    public void Disable()
    {
        PlayerPrefs.SetInt(_currentPrefsName, 1);
        _image.sprite = _inactiveSprite;
        _button.enabled = false;
    }
}
