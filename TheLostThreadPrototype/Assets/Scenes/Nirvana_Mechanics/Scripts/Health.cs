using UnityEngine;

public class Health : MonoBehaviour
{
    //this script will handle the player health only
    
    //These two variables will be needed as player will regenerate health overtime unless the health hits 0
    [SerializeField] private float MaxHealth = 20;
    private float CurrentHealth;
    
    //Animator
    [SerializeField] public Animator fadeAnimator;

    void Awake()
    {
        //Whenever the game is started we want the health to be at the maxHealth as the health will regen anyways
        CurrentHealth = MaxHealth;
    }

    public void Damage(float dmgAmount)
    {
        //health will always be decreased from the currentHealth variable
        CurrentHealth -= dmgAmount;
        if (CurrentHealth <= 0) Death();
    }

    public void SewageDeath()
    {
        CurrentHealth = 0;
        Death();
        //Send player back to check point code goes here!!!
    }

    public void Death()
    {
        fadeAnimator.SetTrigger("fadeInAnim");
        //if(CurrentHealth == 0) Destroy(gameObject);
    }
}
