using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] int maxHealth;
    [SerializeField] Text changingText;
#pragma warning restore 0649
    private int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
        UpdatePlayerHealthText(_currentHealth);
    }

    public void UpdatePlayerHealthText(int currentHealth)
    {
        changingText.text = "Kalan Can: " + currentHealth;
    }

    public void DeductHealth(int damage)
    {
        _currentHealth = _currentHealth - damage;
        UpdatePlayerHealthText(_currentHealth);
        Debug.Log("Akın yavaş!" + _currentHealth);
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        print("Ah! kalbim");
    }

    public void AddHealth(int heal)
    {
        if (_currentHealth < maxHealth)
        {
            _currentHealth = _currentHealth + heal;
            UpdatePlayerHealthText(_currentHealth);
        }
    }
    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
}
