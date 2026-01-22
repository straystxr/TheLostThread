using System;
using UnityEngine;

namespace Scenes.Nirvana_Mechanics.Scripts
{
    public class Rat : MonoBehaviour
    {
        //damage per collision 
        [SerializeField] private float dmgPerColl = 2f;
        //damage cooldown to make the game fairer on the player
        [SerializeField] private float dmgCoolDown = 1f;
        
        private bool canDamage = true;

        //on trigger will continuously stay on as the rats will be continuously jumping on the player
        private void OnTriggerStay(Collider other)
        {
            //calling the health script to get data and putting it in a variable called player
            Health player = other.GetComponent<Health>();
            //setting an early return if player is null or if canDamage is null0
            if (player == null || !canDamage) return; //early return
            
            //setting canDamage to false to temporarily disable damage and to set up a cooldown after
            canDamage = false;
            //player's health data will be updated by -2 dmg 
            player.RatDamage(dmgPerColl);
            //calling the function to turn canDamage true again after a set amount of seconds which is a second
            Invoke(nameof(ResetDamage), dmgCoolDown);
        }

        //function storing bool of canDamage like a toggle
        private void ResetDamage()
        {
            canDamage = true;
        }
    }
}