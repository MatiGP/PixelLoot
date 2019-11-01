﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayableChar_SO character;
    public CharacterStatsUI charactersUI;
    public static PlayerStats instance;

    public int characterCurrentHealth;
    public int characterMaxHealth;
    public int characterCurrentMana;
    private int characterMaxMana;
  
    [HideInInspector]
    public bool canCastSpells;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        characterMaxHealth = character.characterBaseHealth + character.baseStats[3];
        characterCurrentHealth = characterMaxHealth;
        characterMaxMana = character.characterBaseMana + character.baseStats[0];
        characterCurrentMana = characterMaxMana;
    }     
    public void RestoreMana(int manaAmount)
    {
        characterCurrentMana += manaAmount;
        if (characterCurrentMana > characterMaxMana)
        {
            characterCurrentMana = characterCurrentMana - (characterCurrentMana % characterMaxMana);
        }
        canCastSpells = true;
    }
    public void SpendMana(int manaAmount)
    {
        characterCurrentMana -= manaAmount;
    }
    
    public float GetManaPercentage()
    {
        return (float)characterCurrentMana / characterMaxMana;
    }
    public void TakeDamage(int damageTaken)
    {
        characterCurrentHealth -= damageTaken;
        if (characterCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void RestoreHealth(int healthAmount)
    {
        characterCurrentHealth += healthAmount;
        if (characterCurrentHealth > characterMaxHealth)
        {
            characterCurrentHealth = characterCurrentHealth - (characterCurrentHealth % characterMaxHealth);
        }
    }
    public float GetHealthPercentage()
    {
        return (float)characterCurrentHealth / characterMaxHealth;
    }

    private void Update()
    {
        charactersUI.UpdateBars();
    }
}
