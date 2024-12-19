using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSelect : MonoBehaviour
{
    public GameObject cone;
    public GameObject target;
    //public Collider coneCollider;
    private Vector3 mousePosition;
    private List<GameObject> highlightedObjects;
    private List<GameObject> selectedObjects;
    private Vector3 currentConeScale;
    private float coneGrowthRate = 3f;
    Transform newTransform;
    private int upSpeed = 10;
    private int downSpeed = 10;
    void Start()
    {
        //float newDistance =  cone.GetComponent<ConeCollider>().GetDistance();
        //newTransform = new Vector3(newDistance/3,newDistance/3, newDistance);
        //newTransform = new GameObject().transform;
    highlightedObjects = new List<GameObject>();
    selectedObjects = new List<GameObject>();
    }

    void Update()
    {
        // currentConeScale = cone.transform.localScale;
        float newDistance =  cone.GetComponent<ConeCollider>().GetDistance();
        cone.transform.localScale = new Vector3(newDistance/3,newDistance/3, newDistance);
        currentConeScale = cone.transform.localScale;
        if (Input.GetMouseButton(0) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
        {
            newDistance += Time.deltaTime * coneGrowthRate * upSpeed/10;
            cone.GetComponent<ConeCollider>().SetDistance(newDistance);
            cone.transform.localScale = currentConeScale;
            upSpeed+=1;
        }
        else if ((Input.GetMouseButton(1) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown)) && newDistance>=0.5f) {
            newDistance -= Time.deltaTime * coneGrowthRate * downSpeed/10;
            cone.GetComponent<ConeCollider>().SetDistance(newDistance);
            cone.transform.localScale = currentConeScale;
            downSpeed+=1;
        }
        else if (Input.GetMouseButtonDown(2) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            focusSelectedObjects();
        }
        else{
            upSpeed=10;
            downSpeed=10;
        }
        //DetectAndHighlightObjects();
    }

    private void focusSelectedObjects(){
        foreach(var selected in selectedObjects){
            selected.GetComponent<CelestialObject>().OnDeselect();
        }
        selectedObjects = new List<GameObject>();
        foreach(var selected in highlightedObjects){
            selectedObjects.Add(selected);
            selected.GetComponent<CelestialObject>().OnSelect();
        }
       // Debug.LogError(selectedObjects.Count);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject enemy = other.gameObject;
        if(other.CompareTag("Enemy")){
            Debug.Log("Enter");
            enemy.GetComponent<CelestialObject>().OnHover();
            highlightedObjects.Add(enemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject enemy = other.gameObject;
        if(other.CompareTag("Enemy")){
            Debug.Log("Exit");
            highlightedObjects.Remove(enemy);
            enemy.GetComponent<CelestialObject>().OnUnhover();

        }
    }
}

    // Collider[] coneColliderGenerator(GameObject cone){
    //     Vector3 coneCenter = cone.transform.position + cone.transform.forward * (cone.transform.localScale.z / 2);
    //     Vector3 coneSize = new Vector3(cone.transform.localScale.x, cone.transform.localScale.y, cone.transform.localScale.z);
    //     Collider[] colliders = Physics.OverlapBox(coneCenter, coneSize / 2, cone.transform.rotation);
    //     return colliders;
    // }
    // void DetectAndHighlightObjects(){
    //     Collider[] colliders = coneColliderGenerator(cone);
    //     List<GameObject> localEnemies = new List<GameObject>();
    //     foreach (Collider collider in colliders)
    //     {
    //         if (collider.CompareTag("Enemy"))
    //         {
    //             GameObject enemy = collider.gameObject;
    //             localEnemies.Add(enemy);
    //         }
    //     }
        
    //     var allObjects = new HashSet<GameObject>(localEnemies);
    //     allObjects.UnionWith(highlightedObjects);
    //     foreach (GameObject enemy in allObjects){
    //         bool inLocal = localEnemies.Contains(enemy);
    //         bool inHighlighted = highlightedObjects.Contains(enemy);

    //         if(inLocal && inHighlighted){

    //         }
    //         else if(inLocal){
    //             enemy.GetComponent<CelestialObject>().OnHover();
    //             highlightedObjects.Add(enemy);
    //         }
    //         else if(inHighlighted){
    //             enemy.GetComponent<CelestialObject>().OnUnhover();
    //             highlightedObjects.Remove(enemy);
    //         }

    //     }
    // }
    // void DetectAndCloneObjects()
    // {
    //     Vector3 coneCenter = cone.transform.position + cone.transform.forward * (cone.transform.localScale.z / 2);
    //     Vector3 coneSize = new Vector3(cone.transform.localScale.x, cone.transform.localScale.y, cone.transform.localScale.z);

    //     Collider[] colliders = Physics.OverlapBox(coneCenter, coneSize / 2, cone.transform.rotation);

    //     foreach (Collider collider in colliders)
    //     {
    //         if (collider.CompareTag("Enemy"))
    //         {
    //             GameObject enemy = collider.gameObject;
    //             enemy.GetComponent<CelestialObject>().OnSelect();
    //             Debug.Log($"Cloning enemy: {enemy.name}");

    //             GameObject clonedEnemy = Instantiate(enemy);

    //             clonedEnemy.transform.SetParent(target.transform);

    //             clonedEnemy.transform.position = enemy.transform.position;

    //             AddInteractionLink(enemy, clonedEnemy);
    //         }
    //     }
    // }

    // void AddInteractionLink(GameObject original, GameObject clone)
    // {
    //     CelestialObject originalHoverScript = original.GetComponent<CelestialObject>();
    //     CelestialObject cloneHoverScript = clone.GetComponent<CelestialObject>();

    //     if (originalHoverScript != null && cloneHoverScript != null)
    //     {
    //         originalHoverScript.SetLinkedObject(cloneHoverScript);
    //         cloneHoverScript.SetLinkedObject(originalHoverScript);
    //     }
    // }
