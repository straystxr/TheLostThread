using System;
using System.Collections;
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
        CurrentHealth = 0;
        Death();
        //Send player back to check point code goes here!!!
    }

    public void Death()
    {
        //fadeAnimator.SetTrigger("fadeInAnim");
        if (CurrentHealth <= 0)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    private void SelfDestruct()
    {
        //Destroying gameobjct to respawn it back to a different checkpoint
        Debug.Log("DESTROYED");
        Destroy(gameObject); // player should be moved to the closest checkpoint, not destroyed
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2f);
        
        //setting up a variable to fetch the respawn point position and loading it
        Vector3 respawnPosition = CheckpointManager.Instance.LoadPosition();

        //condition: if respawn is not at origin aka Vector3.zero, respawn spawn will be set at last saved
        //checkpoint
        if (respawnPosition != Vector3.zero)
        {
            Debug.Log("Respawning player");
            transform.position = respawnPosition;
        }
        else
        {
            //no checkpoint found
            Debug.Log("Respawn position not found");
            transform.position = Vector3.zero;
        }
        
        //setting currentHealth back to maxHealth
        CurrentHealth = MaxHealth;
    }
}
