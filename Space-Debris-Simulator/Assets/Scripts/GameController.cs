using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] celestialObjects;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleOrbitVisibility();
        }
    }

    private void ToggleOrbitVisibility()
    {
        foreach (GameObject obj in celestialObjects)
        {
            if (obj.GetComponent<Orbit>() != null)
            {
                var orbit = obj.GetComponent<Orbit>();
                if (orbit != null)
                {
                    orbit.ToggleOrbitVisibility(!orbit.GetOrbitRenderer().enabled);
                }
            }
            
        }
    }
}
