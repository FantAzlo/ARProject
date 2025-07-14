using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Drone
{
    public class Player : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private TextMeshProUGUI _deathScoreText;
        [SerializeField] private AudioSource _deathTheme;
        [SerializeField] private AudioSource _battleTheme;
        [SerializeField] private GameObject _deathScreen;

        private static Player _instance;
        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new NullReferenceException($"{nameof(SpawnManager)} instance is not assigned");
                }
                return _instance;
            }
        }

        public float Score
        {
            get { return _score; }
            set
            {
                _score = value;
                _scoreText.text = _score.ToString("#"); 
            }
        }

        private float _energy = 120;
        public float Energy { 
            get { return _energy; } 
            set 
            { 
                _energy = value; 
                if (_energy <= 0) GameOverLogic(); 
                _energyText.text = _energy.ToString("#");
            } 
        }

        public bool EnergySpending;
        private float _score;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            StartCoroutine(EnenrgyWasting());
        }

        private IEnumerator EnenrgyWasting()
        {
            while (true) 
            {
                if (EnergySpending)
                    Energy--;
                yield return new WaitForSeconds(1f);
            };
        }

        private void GameOverLogic()
        {
            _deathScoreText.text = Score.ToString("#");
            _deathScreen.SetActive(true);
            _battleTheme.Stop();
            _deathTheme.Play();
            SpawnManager.Instance.DestroyAllObjects();
            Destroy(gameObject);
        }

        public float TakeDamage(float dmg)
        {
            Energy-= dmg;
            return dmg;
        }
    }
}