using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{    
    public class LifePowerUp : PowerUp
    {
        protected override void PowerUpPlayer(GameObject player)
        {
            Health playerHealth = player.GetComponent<Health>();

            playerHealth.IncreaseHealth(20);
          
        }

    }
}
