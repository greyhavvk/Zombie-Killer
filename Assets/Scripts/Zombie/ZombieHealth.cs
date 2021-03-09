using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] int startHealth = 100;
    [SerializeField] Text changingText;
#pragma warning restore 0649
    private int _currentHealth;
    void Start()
    {
        _currentHealth = startHealth;
        UpdateZombieHealthText(_currentHealth);
    }

    public void UpdateZombieHealthText(int currentHealth)
    {
        changingText.text = "Zombinin kalan canı: " + currentHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
    public void Hit(int damage)
    {
        _currentHealth -= damage;
        UpdateZombieHealthText(_currentHealth);
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            Debug.Log("Zombie killed");
        }
        Debug.Log("Zombie get damage" + _currentHealth);
    }
}
