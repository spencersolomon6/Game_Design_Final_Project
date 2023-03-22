using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    [SerializeField]
    public AudioClip deathAudio;
    [SerializeField]
    public Slider healthBar;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmt)
    {
        currentHealth -= damageAmt;
        healthBar.value = Mathf.Clamp(currentHealth, 0, 100);

        AudioSource.PlayClipAtPoint(deathAudio, transform.position);

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
    }

    public void AddHealth(int healthAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthAmount, 0, 100);
        healthBar.value = currentHealth;
    }

    void PlayerDies()
    {
        Debug.Log("Player is dead!");
        transform.Rotate(-90, 0, 0, Space.Self);
        Camera.main.GetComponent<LevelManager>().GameLost();
    }
}
