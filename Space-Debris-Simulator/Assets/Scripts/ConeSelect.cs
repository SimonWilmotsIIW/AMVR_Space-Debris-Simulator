using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSelect : MonoBehaviour
{
    public GameObject cone;
    public Collider coneCollider;
    private Vector3 mousePosition;

    private Vector3 startConeScale;
    private Vector3 startConePosition;
    private Vector3 currentConeScale;
    private float coneGrowthRate = 3f;
    void Start()
    {
        startConePosition = cone.transform.position;
        startConeScale = cone.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        currentConeScale = cone.transform.localScale;
        if (Input.GetMouseButton(0))
        {
            currentConeScale.z += Time.deltaTime * coneGrowthRate;
            cone.transform.localScale = currentConeScale;
        } else if (Input.GetMouseButton(1)) {
            currentConeScale.z -= Time.deltaTime * coneGrowthRate;
            cone.transform.localScale = currentConeScale;
        }
    }
}
