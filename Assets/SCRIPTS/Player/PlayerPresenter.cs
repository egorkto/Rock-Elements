using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Image _healthBar;

    private Health _currentHealth;

    public void SetHealth(Health health)
    {
        _currentHealth = health;
        PresentHealth();
    }

    public void PresentHealth()
    {
        if (_currentHealth == null)
            Debug.LogError("Health is null!");
        var currentHealth = Mathf.Clamp(_currentHealth.CurrentHealth, 0, int.MaxValue);
        _healthText.text = currentHealth.ToString() + "/" + _currentHealth.MaxHealth.ToString();
        _healthBar.fillAmount = (float)currentHealth / _currentHealth.MaxHealth;
    }
}
