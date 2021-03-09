using UnityEngine;

public class DetectItems : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealthItem"))
        {
            _playerHealth.AddHealth(10);
            other.gameObject.SetActive(false);
            Debug.Log("Oh çok iyi geldi: " + _playerHealth.GetCurrentHealth());
        }
    }
}
