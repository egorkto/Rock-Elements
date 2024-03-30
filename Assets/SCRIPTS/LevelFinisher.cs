using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinisher : MonoBehaviour
{
    [SerializeField] private PlayersSwitcher _switcher;

    private void OnEnable()
    {
        _switcher.AllPlayersDied += Lose;
    }

    private void OnDisable()
    {
        _switcher.AllPlayersDied -= Lose;
    }

    private void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
