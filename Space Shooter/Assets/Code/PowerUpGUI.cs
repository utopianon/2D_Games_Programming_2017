using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class PowerUpGUI : MonoBehaviour
    {
        Text text;
        public PlayerSpaceShip player;

        private void Start()
        {
            text = GetComponent<Text>();
        }
        private void Update()
        {
            if(player == null)
            player = GameObject.Find("PlayerUnit(Clone)").GetComponent<PlayerSpaceShip>();
            else
            text.text =  "Weapon Timer: " + player.PowerUpTimer + "\nPlayer Life: " + player.Health.CurrentHealth ;
        }
    }
}
