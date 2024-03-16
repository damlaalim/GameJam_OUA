using UnityEngine;
using _game.Scripts.Player;

namespace _game.Scripts.Enemy
{
    public class EnemyColliderArea : MonoBehaviour
    {
        private EnemyController _enemy;

        private void Awake()
        {
            _enemy = GetComponent<EnemyController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
                _enemy.IsChasing = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
                _enemy.IsChasing = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent<PlayerController>(out var player))
                _enemy.CatchPlayer();
        }
    }
}