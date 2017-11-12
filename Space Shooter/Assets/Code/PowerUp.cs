using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    
    public abstract class PowerUp : MonoBehaviour
    {
        Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();   
        }

        private void FixedUpdate()
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 velocity = new Vector2(0, -1);

            Vector2 newPosition = currentPosition + velocity * Time.fixedDeltaTime;
            _rigidbody.MovePosition(newPosition);


        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                PowerUpPlayer(collision.gameObject);
                Destroy(gameObject);
            }
        }

        protected abstract void PowerUpPlayer(GameObject player);

    }
}
