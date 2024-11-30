using UnityEngine;

public class CelestialObject : MonoBehaviour
{
    public string objectName;
    public string metadata;
    public float sizeMultiplier = 1.0f;

    private void Start()
    {
        transform.localScale = Vector3.one * sizeMultiplier;
    }
}
