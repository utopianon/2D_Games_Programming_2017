using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class PlayerSpaceShip : SpaceShipBase
	{
		public const string HorizontalAxis = "Horizontal";
		public const string VerticalAxis = "Vertical";
		public const string FireButtonName = "Fire1";

        private Vector3 _starPosition;

        [SerializeField]
        protected int _lives;
        
        //boolean to start flashing
        protected bool _invicible;
        [SerializeField]
        protected float _flashSpeed = 0.1f;
               
        protected override void Awake()
        {
            base.Awake();
            _starPosition = transform.position;
            _lives = 3;
        }

        public override Type UnitType
		{
			get { return Type.Player; }
		}

		private Vector3 GetInputVector()
		{
			float horizontalInput = Input.GetAxis(HorizontalAxis);
			float verticalInput = Input.GetAxis(VerticalAxis);

			return new Vector3(horizontalInput, verticalInput);
		}

		protected override void Update()
		{
			base.Update();

			if(Input.GetButton(FireButtonName))
			{
				Shoot();
			}

        }

        protected override void Move()
		{
			Vector3 inputVector = GetInputVector();
			Vector2 movementVector = inputVector * Speed;
			transform.Translate(movementVector * Time.deltaTime);
		}

        //overrides die method, adds the respanw if player has lives left
        protected override void Die()
        {
            if (_lives > 0)
            {
                Respawn();
            }
            else
                base.Die();
        }

        //takes one life away and returns player to starting position
        protected void Respawn ()
        {
            StartCoroutine(Flash(_flashSpeed));
            transform.position = _starPosition;
            Health.IncreaseHealth(Health.MaxHealth);
            _lives -= 1;
        }

        IEnumerator Flash(float FlashSpeed)
        {
            //turns off collider for invincibility
            _collider.enabled = false;

            //makes it flash
            for (int i = 0; i < 3; i++){
                _sprite.enabled = false;
                yield return new WaitForSeconds(FlashSpeed);
                _sprite.enabled = true;
                yield return new WaitForSeconds(FlashSpeed);

            }

            _collider.enabled = true;
        }
    }
}
