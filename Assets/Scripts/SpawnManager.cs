using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float _spawnRangeMax = 2f;
    [SerializeField] 
    private float _spawnRangeMin = 1f;

    [SerializeField]
    private List<Spawner> _spawners = new List<Spawner>();

    private List<Coroutine> _coroutines = new List<Coroutine>();
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    public bool _isSpawning = true;
    public float Timer = 0f;
    private static SpawnManager _instance;

    public static SpawnManager Instance
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

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        foreach (var spawner in _spawners)
        {
            _coroutines.Add(StartCoroutine(SpawnCoroutine(spawner)));
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        Timer =+ Time.deltaTime;
    }

    private void StopSpawn()
    {
        _isSpawning = false;
    }

    public void DestroyAllObjects()
    {
        foreach (var obj in _spawnedObjects)
        {
            Destroy(obj);
        }
    }

    private IEnumerator SpawnCoroutine(Spawner spawner)
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            if (_isSpawning)
            {
                var position = Drone.Player.Instance.transform.position + UnityEngine.Random.onUnitSphere
                   * UnityEngine.Random.Range(_spawnRangeMin, _spawnRangeMax) * spawner.RangeCooficient;
                var obj = Instantiate(spawner.SpawnList[UnityEngine.Random.Range(0, spawner.SpawnList.Count)], position: position, Quaternion.identity);
                _spawnedObjects.Add(obj);
            }
            yield return new WaitForSeconds(spawner.SpawnDelay * (1 - Timer / 60f));
        }
    }

    [Serializable]
    private struct Spawner
    {
        [SerializeField]
        public List<GameObject> SpawnList;
        [SerializeField]
        public float SpawnDelay;
        [SerializeField]
        public float RangeCooficient;
    } 
}
