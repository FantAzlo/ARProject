using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Drone
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Player))]
    public class DroneController : MonoBehaviour
    {
        [Header("ActionReferences")]
        [SerializeField]
        private InputActionReference _veritcalAxisAction;
        [SerializeField]
        private InputActionReference _rotationAction;
        [SerializeField]
        private InputActionReference _horizontalAxisAction;
        [SerializeField]
        private InputActionReference _attackActionReference;

        [Header("Vertical movement settings")]
        [SerializeField]
        private float _verticalCoeficient = 1;

        [Header("Horizontal movement settings")]
        [SerializeField]
        private float _horizontalCoeficient = 1;

        [Header("Rotation settings")]
        [SerializeField]
        private float _rotationCoeficient = 1;

        [Header("Common")]
        [SerializeField]
        private AudioSource _shootAudioSource;
        [SerializeField]
        private GameObject _visual;
        [SerializeField]
        public Player Player;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private float _shootingTime;
        [SerializeField]
        private GameObject _spawnPoint;
        [SerializeField]
        private float _bulletSpeed;

        private Rigidbody _rigidbody;
        private bool _isShooting = false;
        private AudioSource _droneSound;

        void Start()
        {
            Player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = _visual.GetComponent<Animator>();
            StartCoroutine(Shooting());
            _droneSound = GetComponent<AudioSource>();
        }

        void Update()
        {
            Rotation();
            Movement();
            Attack();
        }

        private void Rotation()
        {
            var torque = _rotationAction.action.ReadValue<float>();
            //_rigidbody.AddRelativeTorque(torque * Vector3.up * _rotationCoeficient * Time.deltaTime, ForceMode.Force);
            _rigidbody.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * torque * 2.4f);
        }

        private void Attack()
        {
            if (_attackActionReference.action.IsPressed())
            {
                _isShooting = true;
            }
            else _isShooting = false;
        }

        private void Movement()
        {
            var value = _horizontalAxisAction.action.ReadValue<Vector2>();
            var force = new Vector3(value.x, _veritcalAxisAction.action.ReadValue<float>(), value.y);
            _rigidbody.AddRelativeForce(force * _horizontalCoeficient, ForceMode.Force);
            _animator.SetFloat("RotatingSpeed", force.magnitude + 1);
            _droneSound.pitch = Mathf.Clamp(force.magnitude, 0.9f, 1.1f);
            _visual.transform.localRotation = Quaternion.Lerp(_visual.transform.localRotation, Quaternion.Euler(45 * - new Vector3(-force.z, 0, force.x)), Time.deltaTime);
        }

        private IEnumerator Shooting()
        {
            while (true) 
            {
                if (_isShooting)
                {
                    Debug.Log("sad");

                    var bullet = Instantiate(_bulletPrefab, _spawnPoint.transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletSpeed;
                    _shootAudioSource.Play();
                }
                yield return new WaitForSeconds(_shootingTime);
            }
        }
    }
}

