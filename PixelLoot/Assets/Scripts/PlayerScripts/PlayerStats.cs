﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayableChar_SO character;
    public CharacterStatsUI charactersUI;
    public static PlayerStats instance;
    public GameObject deathPanel;


    public int characterCurrentHealth;
    public int characterMaxHealth;
    public int characterCurrentMana;
    public PlayerStatsAndItems playerSAI;
    private int characterMaxMana;
    private int armor;
    private SpriteRenderer spriteRenderer;
    private PlayerController controller;
    [HideInInspector]
    public bool canCastSpells;
    public static bool isPlayerAlive;
    void Awake()
    {                 
        instance = this;           
        

        DeathMenuScript.isPlayerDead = false;
        characterMaxHealth = character.characterBaseHealth + character.baseStats[3];
        characterCurrentHealth = characterMaxHealth;
        characterMaxMana = character.characterBaseMana + character.baseStats[0];
        characterCurrentMana = characterMaxMana;
        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }     
    public void RestoreMana(int manaAmount)
    {
        characterCurrentMana += manaAmount;

        if (characterCurrentMana > characterMaxMana)
        {
            characterCurrentMana = characterCurrentMana - (characterCurrentMana % characterMaxMana);
        }
        charactersUI.UpdateBars();
    }
    public void SpendMana(int manaAmount)
    {
        characterCurrentMana -= manaAmount;
        charactersUI.UpdateBars();
    }
    
    public float GetManaPercentage()
    {
        return (float)characterCurrentMana / characterMaxMana;
    }
    public void TakeDamage(int damageTaken)
    {
        characterCurrentHealth -= (damageTaken - armor);
        charactersUI.UpdateBars();

        changeColorRed();
        if (characterCurrentHealth <= 0)
        {
            DeathMenuScript.isPlayerDead = true;
            controller.vcam.enabled = isPlayerAlive;

            Destroy(gameObject);
        }
        StartCoroutine(changeColorWhite());
    }

    public void RestoreHealth(int healthAmount)
    {
        characterCurrentHealth += healthAmount;
        if (characterCurrentHealth > characterMaxHealth)
        {
            characterCurrentHealth = characterCurrentHealth - (characterCurrentHealth % characterMaxHealth);
        }
        charactersUI.UpdateBars();
    }
    public float GetHealthPercentage()
    {
        return (float)characterCurrentHealth / characterMaxHealth;
    }

    private void Update()
    {
        
        if (isPlayerAlive)
        {
            AttachTheCamera();
            isPlayerAlive = false;
        }

    }

    public void AttachTheCamera()
    {
        controller.vcam.enabled = true;
    }
    
    void changeColorRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    IEnumerator changeColorWhite()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        yield return null;
    }

    public void SetArmor(int armor)
    {
        this.armor = armor;
    }

    public void SaveState()
    {
        playerSAI.currentHealth = characterCurrentHealth;
        playerSAI.currentMana = characterCurrentMana;
        playerSAI.itemsInInventory = Inventory.instance.items;
    }

    public void LoadState()
    {
        characterCurrentHealth = playerSAI.currentHealth;
        characterCurrentMana = playerSAI.currentMana;
        Inventory.instance.items = playerSAI.itemsInInventory;
        Inventory.instance.ui.UpdateUI();
    }
}
