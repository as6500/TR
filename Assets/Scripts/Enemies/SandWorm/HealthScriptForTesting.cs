// using UnityEngine;
// using TMPro;
// using Unity.VisualScripting;
// using UnityEngine.UIElements;
//
//THIS SCRIPT IS FOR TESTING AND WILL BE DISABLED WHEN THE BETTER HEALTH SCRIPT IS CONNECTED

// public class HealthScriptForTesting : MonoBehaviour
// {
//     public float maxHealth = 100f;
//     public float currentHealth;
//     [SerializeField] private TMP_Text healthText;
//
//     void Start()
//     {
//         currentHealth = maxHealth;
//         UpdateHealthText();
//     }
//
//     private void UpdateHealthText()
//     {
//         if (healthText != null)
//         {
//             healthText.text = "Health: " + currentHealth + "/" + maxHealth;
//         }
//     }
//
//     public void TakeDamage(float damage)
//     {
//         currentHealth = Mathf.Max(currentHealth - damage, 0);
//         UpdateHealthText();
//     }
//
//     public void HealDamage(float amount)
//     {
//         currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
//         UpdateHealthText();
//     }
//
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.L))
//         {
//             TakeDamage(5);
//         }
//         
//         if (Input.GetKeyDown(KeyCode.K))
//         {
//             HealDamage(5);
//         }
//     }
//     
// }