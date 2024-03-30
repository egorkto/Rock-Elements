using System.Collections;
using UnityEngine;

public class EnemiesWave : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private float _speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            StartCoroutine(MoveToPlayer(player.transform));
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.TryGetComponent(out Player player))
            StopAllCoroutines();
    }

    private IEnumerator MoveToPlayer(Transform player)
    {
        Vector2 moveDirection = Vector3.zero;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, _speed * Time.deltaTime);
            //transform.LookAt(player.transform);
            yield return new WaitForFixedUpdate();
        }
    }
}
