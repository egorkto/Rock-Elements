using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalMap : MonoBehaviour
{
    [SerializeField] private MapSpot[] _spots;

    private void Start()
    {
        for (int i = 0; i < _spots.Length; i++)
            _spots[i].Initialize(i);
    }

    private void OnEnable()
    {
        MapSpot.Clicked += ApplySpot;
    }

    private void OnDisable()
    {
        MapSpot.Clicked -= ApplySpot;
    }

    private void ApplySpot(MapSpot spot)
    {
        spot.Disable();
        SceneManager.LoadScene(spot.SceneIndex);
    }
}
