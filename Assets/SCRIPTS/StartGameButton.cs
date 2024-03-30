using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private int _mapSceneIndex;

    public void Click()
    {
        SceneManager.LoadScene(_mapSceneIndex);
    }
}
