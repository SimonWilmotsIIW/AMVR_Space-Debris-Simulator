using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionOfObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectWithRaycast()
    {
        CelestialObject celestialObject = gameObject.GetComponent<CelestialObject>();
        Debug.Log($"Selected: {celestialObject.name}, {celestialObject.metadata}");
    }
}
