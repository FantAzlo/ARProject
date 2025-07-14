using UnityEngine;
using Drone;

public class BonusLogic : MonoBehaviour
{
    [SerializeField]
    private float _bonusSize = 10f;
    [SerializeField]
    private GameObject _deathEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.gameObject.TryGetComponent<Player>(out var player))
        {
            player.Energy += _bonusSize;
            player.Score += _bonusSize * 10;
            var a = Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(a, 2f);
            Destroy(gameObject);
        }
    }
 
}
