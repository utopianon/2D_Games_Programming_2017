using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class WeaponPowerUp : PowerUp
    {
        protected override void PowerUpPlayer(GameObject player)
        {
            PlayerSpaceShip _player = player.GetComponent<PlayerSpaceShip>();

            _player.AddPowerUpTimer(5);
        }
    }
}
