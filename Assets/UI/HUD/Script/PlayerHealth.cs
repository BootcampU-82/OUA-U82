using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 5;
    private int currentHealth;

    private float damageCooldown = 5f;
    private bool isDead = false; 
 
    private float lastDamageTime; 
    private int damageCount = 0;

    public GameObject FailBlurEffectObject;
    public GameObject FailScene; // UI fail
    public Animator DamageEffectAnim;


    private void Start()
    {
        
    }


    private void Update()
    {

    }


    public void TakeDamage()
    {
        if (isDead) return; 

        // Eðer son hasar alma zamanýndan geçen süre hasar alma süresi aralýðýný aþýyorsa, hasar alma sayýsýný sýfýrla
        if (Time.time - lastDamageTime > damageCooldown)
        {
            damageCount = 0;
        }

        DamageEffectAnim.SetTrigger("Damage");
        damageCount++;
        lastDamageTime = Time.time;

        if (damageCount >= maxHealth)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        FailScene.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //FailBlurEffectObject.SetActive(true);
    }
}
