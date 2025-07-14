using UnityEngine;

public class RadarPlane : MonoBehaviour
{
    public static RadarPlane Instance;
    public void Awake()
    {
        Instance = this;
    }
}