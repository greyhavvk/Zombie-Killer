using UnityEngine;
using UnityEngine.AI;

enum ZombieState
{
    Idle = 0,
    Walk = 1,
    Dead = 2,
    Attack = 3
}
public class ZombieAI : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;
    private ZombieState _zombieState;
    private GameObject _playerObject;
    private PlayerHealth _playerHealth;
    private ZombieHealth _zombieHealth;
    void Start()
    {
        _zombieHealth = GetComponent<ZombieHealth>();
        _playerObject = GameObject.FindWithTag("Player");
        _playerHealth = _playerObject.GetComponent<PlayerHealth>();
        _zombieState = ZombieState.Idle;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_zombieHealth.GetCurrentHealth() <= 0)
        {
            SetState(ZombieState.Dead);
        }
        switch (_zombieState)
        {
            case ZombieState.Dead:
                KillZombie();
                break;
            case ZombieState.Attack:
                Attack();
                break;
            case ZombieState.Walk:
                SearchForTarget();
                break;
            case ZombieState.Idle:
                SearchForTarget();
                break;
            default:
                break;
        }
    }

    private void Attack()
    {
        SetState(ZombieState.Attack);
        _agent.isStopped = true;
    }

    void MakeAttack()
    {
        _playerHealth.DeductHealth(10);
        SearchForTarget();
    }

    private void SearchForTarget()
    {
        float distance = Vector3.Distance(transform.position, _playerObject.transform.position);
        if (distance < 1.5f)
        {
            Attack();
        }
        else if (distance < 10)
        {
            MoveToPlayer();
        }
        else
        {
            SetState(ZombieState.Idle);
            _agent.isStopped = true;
        }
    }

    private void SetState(ZombieState state)
    {
        _zombieState = state;
        _animator.SetInteger("state", (int)state);
    }

    private void MoveToPlayer()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_playerObject.transform.position);
        //agent.Move();
        SetState(ZombieState.Walk);
    }

    private void KillZombie()
    {
        SetState(ZombieState.Dead);
        _agent.isStopped = true;
        Destroy(gameObject, 5);
    }
}
