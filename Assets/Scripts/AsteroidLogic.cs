using Drone;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class AsteroidLogic : MonoBehaviour, ITakeDamage
{
    [SerializeField] private GameObject _visual;
    [SerializeField] private float _hp = 4;
    [SerializeField] private float _maxHp = 20;
    [SerializeField] private float _startImpulse = 100;
    [SerializeField] private float _hpCooficient = 0.0005f;
    [SerializeField] private GameObject _deathEffect;
    private Vector3 _baseScale;
    public float Hp
    {
        get => _hp; 
        set 
        { 
            _hp = value;
            if (_hp % 4 == 0)
                _visual.transform.localScale = _baseScale * _hp * _hpCooficient;
            if (_hp <= 3)
            {
                var a = Instantiate(_deathEffect, transform.position, Quaternion.identity);
                Destroy(a, 2f);
                Drone.Player.Instance.Score += 200;
                Destroy(gameObject);
            }
        }
    }

    private Rigidbody _rigidbody;

    public float TakeDamage(float dmg)
    {
        Hp -= dmg;
        return dmg;
    }

    // Start is called before the first frame update
    void Start()
    {
        var difficulty = 1 + SpawnManager.Instance.Timer / 60f;
        _rigidbody = GetComponent<Rigidbody>();
        _baseScale = _visual.transform.localScale;
        Hp = Random.Range(Hp, _maxHp);
        _visual.transform.localScale = _baseScale * _hp * _hpCooficient * difficulty;
        _rigidbody.mass = _hp * _hpCooficient;
        _rigidbody.AddForce((- gameObject.transform.position + Player.Instance.transform.position).normalized * _startImpulse * difficulty, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent<Player>(out var player))
        {
            player.TakeDamage(_hp);
            Hp = 0;
        }
    }
}
