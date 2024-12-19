using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSelect : MonoBehaviour
{
    public GameObject cone;
    public GameObject target;
    //public Collider coneCollider;
    private Vector3 mousePosition;

    private Vector3 currentConeScale;
    private float coneGrowthRate = 3f;
    Transform newTransform;
    void Start()
    {
        //float newDistance =  cone.GetComponent<ConeCollider>().GetDistance();
        //newTransform = new Vector3(newDistance/3,newDistance/3, newDistance);
        //newTransform = new GameObject().transform;

    }

    void Update()
    {
        // currentConeScale = cone.transform.localScale;
        float newDistance =  cone.GetComponent<ConeCollider>().GetDistance();

        
        Debug.LogError(newDistance);
        cone.transform.localScale = new Vector3(newDistance/3,newDistance/3, newDistance);
        Debug.LogError(cone.transform.localScale);

        currentConeScale = cone.transform.localScale;
        if (Input.GetMouseButton(0))
        {
            newDistance += Time.deltaTime * coneGrowthRate;
            cone.GetComponent<ConeCollider>().SetDistance(newDistance);
            cone.transform.localScale = currentConeScale;
        } else if (Input.GetMouseButton(1)) {
            newDistance -= Time.deltaTime * coneGrowthRate;
            cone.GetComponent<ConeCollider>().SetDistance(newDistance);
            cone.transform.localScale = currentConeScale;
            
        }
        else if (Input.GetMouseButtonDown(2))
        {
            DetectAndCloneObjects();
        }
    }

    void DetectAndCloneObjects()
    {
        Vector3 coneCenter = cone.transform.position + cone.transform.forward * (cone.transform.localScale.z / 2);
        Vector3 coneSize = new Vector3(cone.transform.localScale.x, cone.transform.localScale.y, cone.transform.localScale.z);

        Collider[] colliders = Physics.OverlapBox(coneCenter, coneSize / 2, cone.transform.rotation);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                GameObject enemy = collider.gameObject;
                Debug.Log($"Cloning enemy: {enemy.name}");

                GameObject clonedEnemy = Instantiate(enemy);

                clonedEnemy.transform.SetParent(target.transform);

                clonedEnemy.transform.position = enemy.transform.position;

                AddInteractionLink(enemy, clonedEnemy);
            }
        }
    }

    void AddInteractionLink(GameObject original, GameObject clone)
    {
        CelestialObject originalHoverScript = original.GetComponent<CelestialObject>();
        CelestialObject cloneHoverScript = clone.GetComponent<CelestialObject>();

        if (originalHoverScript != null && cloneHoverScript != null)
        {
            originalHoverScript.SetLinkedObject(cloneHoverScript);
            cloneHoverScript.SetLinkedObject(originalHoverScript);
        }
    }
}