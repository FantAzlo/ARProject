using UnityEngine;
using UnityEngine.VFX;

public class LifeTime : MonoBehaviour
{
    public float LifeTimeSeconds = 2;
    public VisualEffect DeathEffect = null;

    // Update is called once per frame
    void Update()
    {
        LifeTimeSeconds -= Time.deltaTime;
        if (LifeTimeSeconds < 0 )
            //DeathEffect?.Play();
            Destroy(gameObject);
    }
}
