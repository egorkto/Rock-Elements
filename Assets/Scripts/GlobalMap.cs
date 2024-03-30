using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalMap : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        MapSpot.Activated += ApplySpot;
    }

    private void OnDisable()
    {
        MapSpot.Activated -= ApplySpot;
    }

    private void ApplySpot(MapSpot spot)
    {
        SceneManager.LoadScene(spot.SceneIndex);
        gameObject.SetActive(false);
        spot.Disable();
    }
}
