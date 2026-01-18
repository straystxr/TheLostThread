using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //this script will handle the player health only and MUST BE attached to the Player
    
    //These two variables will be needed as player will decrease health and need an upper boundary of a maxhealth
    [SerializeField] private float MaxHealth = 20;
    private float CurrentHealth;
    
    //Animator
    [SerializeField] public Animator fadeAnimator;
    
    void Awake()
    {
        //Whenever the game is started we want the health to be at the maxHealth as the health will regen anyways
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        
    }

    public void RatDamage(float dmgAmount)
    {
        //health will always be decreased from the currentHealth variable
        CurrentHealth -= dmgAmount;
        Debug.Log($"Damage inflicted: {dmgAmount}");
        Debug.Log($"Current Health: {CurrentHealth}");
        if (CurrentHealth <= 0) Death();
        
        //need to set animation to showcase damage inflicted
        
    }

    public void SewageDeath()
    {
        MaxHealth = 0;
        Death();
        //Send player back to check point code goes here!!!
    }

    public void Death()
    {
        fadeAnimator.SetTrigger("fadeInAnim");
        if (MaxHealth <= 0)
        {
            Invoke(nameof(SelfDestruct), 4f);
        }
    }

    private void SelfDestruct()
    {
        //Destroying gameobjct to respawn it back to a different checkpoint
        Destroy(gameObject);
    }
}
