using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using _game.Scripts.Player;
using Zenject;
using Random = UnityEngine.Random;

namespace _game.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public bool IsChasing
        {
            get => _isChasing;
            set
            {
                if (value == false)
                {
                    if (_chaseRoutine is not null)
                        StopCoroutine(_chaseRoutine);
                    
                    _chaseRoutine = StartCoroutine(ChaseReturnFalseRoutine());
                    return;
                }
                
                if (_chaseRoutine is not null)
                    StopCoroutine(_chaseRoutine);

                _isChasing = value;
            }   
        }
        
        [SerializeField] private LayerMask _groundLayer, _playerLayer;
        [SerializeField] private float _sightRange, _walkPointRange;
        [SerializeField] private MeshRenderer _triggerAreaMesh;
        [SerializeField] private Color _dangerColor, _safeColor;

        [Inject] private PlayerController _player;

        private Vector3 _walkPoint;
        private NavMeshAgent _agent;
        private bool _walkPointSet, _playerInSightRange, _isChasing;
        private Coroutine _chaseRoutine;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerLayer);

            if (IsChasing) ChasePlayer();
            else Patrolling();
        }

        private void Patrolling()
        {
            if (!_walkPointSet) SearchWalkPoint();

            if (_walkPointSet)
                _agent.SetDestination(_walkPoint);

            var distanceToWalkPoint = transform.position - _walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
                _walkPointSet = false;

            _triggerAreaMesh.material.color = _safeColor;
        }

        private void SearchWalkPoint()
        {
            var randomZ = Random.Range(-_walkPointRange, _walkPointRange);
            var randomX = Random.Range(-_walkPointRange, _walkPointRange);
            var pos = transform.position;

            _walkPoint = new Vector3(pos.x + randomX, pos.y, pos.z + randomZ);

            if (Physics.Raycast(_walkPoint, -transform.up, 2f, _groundLayer))
                _walkPointSet = true;
        }

        private void ChasePlayer()
        {
            _agent.SetDestination(_player.transform.position);
            _triggerAreaMesh.material.color = _dangerColor;
        }

        private IEnumerator ChaseReturnFalseRoutine()
        {
            yield return new WaitForSeconds(3);
            _isChasing = false;
        }
        
        public void CatchPlayer()
        {
            _isChasing = false;
            _agent.SetDestination(transform.position);
            _player.Dead();
        }
    }
}