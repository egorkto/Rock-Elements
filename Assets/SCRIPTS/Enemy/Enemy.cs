using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackInterval;
    [SerializeField] private int _damage;

    private bool _canAttack = true;
    private PlayersSwitcher _switcher;
    private Coroutine _moveCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayersSwitcher switcher))
        {
            _switcher = switcher;
            _moveCoroutine = StartCoroutine(MoveToPlayer(switcher.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayersSwitcher switcher) && _moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _switcher = null;
        }
    }

    private IEnumerator MoveToPlayer(Transform player)
    {
        Vector2 moveDirection = Vector3.zero;
        while (true)
        {
            if (Vector3.Distance(player.position, transform.position) > _attackDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, _speed * Time.deltaTime);
                transform.LookAt(player.transform);
            }
            else if (_canAttack)
            {
                _switcher.DamageCurrentPlayer(_damage);
                StartCoroutine(WaitAttackDelay());
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator WaitAttackDelay()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackInterval);
        _canAttack = true;
    }
}