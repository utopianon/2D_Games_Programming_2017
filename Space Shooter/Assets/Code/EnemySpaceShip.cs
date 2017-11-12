using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemySpaceShip : SpaceShipBase
    {
        [SerializeField]
        private float _reachedDistance = 0.5f;

        [SerializeField]
        private GameObject _powerUpPrefab1;

        [SerializeField]
        private GameObject _powerUpPrefab2;

        [SerializeField]
        private int _dropChance;

        private GameObject[] _movementTargets;
        private int _currentMovementTargetIndex = 0;

        public Transform CurrentMovementTarget
        {
            get
            {
                return _movementTargets[_currentMovementTargetIndex].transform;
            }
        }

        public override Type UnitType
        {
            get { return Type.Enemy; }
        }

        protected override void Update()
        {
            base.Update();

            Shoot();
        }

        public void SetMovementTargets(GameObject[] movementTargets)
        {
            _movementTargets = movementTargets;
            _currentMovementTargetIndex = 0;
        }

        protected override void Move()
        {
            if (_movementTargets == null || _movementTargets.Length == 0)
            {
                return;
            }

            UpdateMovementTarget();
            Vector3 direction =
                (CurrentMovementTarget.position - transform.position).normalized;
            transform.Translate(direction * Speed * Time.deltaTime);
        }

        private void UpdateMovementTarget()
        {
            // Have we reached our current movement target or not?
            if (Vector3.Distance(transform.position,
                CurrentMovementTarget.position) < _reachedDistance)
            {
                // We have reached the target, let's update it.
                if (_currentMovementTargetIndex >= _movementTargets.Length - 1)
                {
                    // we have reached the end of our path. Let's use the first target point
                    // as our next target.
                    _currentMovementTargetIndex = 0;
                }
                else
                {
                    _currentMovementTargetIndex++;
                }
            }
        }

        private void OnDestroy()
        {
            int rand = Random.Range(0, 100);

            if (rand <= _dropChance)
            {
                int powerUptoSpawn = Random.Range(0, 10);
                if (powerUptoSpawn <= 6)
                {
                    Instantiate(_powerUpPrefab1, transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(_powerUpPrefab2, transform.position, transform.rotation);
                }
            }
        }
    }
}
