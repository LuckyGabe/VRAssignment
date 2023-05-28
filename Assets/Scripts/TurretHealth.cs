using UnityEngine;
using TMPro;

public class TurretHealth : MonoBehaviour
{
    public int maxHealth = 300;
    [SerializeField]
    private int currentHealth;
    public TextMeshProUGUI healthText;
    public bool bIsDead = false;
    public GameManager gameManager;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (bIsDead) { gameManager.bGameOver = true; }
        if (currentHealth > 0) { healthText.text = currentHealth.ToString(); }
        else healthText.text = "0";
    }

    //Method that deacrease the health
    public void DealDamage(int damage)
    {

        if (currentHealth - damage <= 0)
        {
            currentHealth = 0;
            bIsDead = true;
        }
        else if (gameObject.activeInHierarchy)
        {
            currentHealth -= damage;

        }
    }
}
