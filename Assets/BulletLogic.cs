using UnityEngine;
using UnityEngine.VFX;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffectPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.TryGetComponent<AsteroidLogic>(out var asteroidLogic))
        {
            var a = Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(a, 2f);
            asteroidLogic.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
