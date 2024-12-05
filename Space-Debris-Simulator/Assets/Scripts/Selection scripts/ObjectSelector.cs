using UnityEngine;

public class VRObjectSelector : MonoBehaviour
{
    private Camera vrCamera;
    public LayerMask interactableLayer;
    public float selectionDistance = 100f;

    public LineRenderer rayVisualizer;
    private Shader newShader;

    private void Start()
    {
        vrCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();

        newShader = Shader.Find("Mobile/Unlit");

        if (rayVisualizer != null && newShader != null)
        {
            Material newMaterial = new Material(newShader);
            rayVisualizer.material = newMaterial;
        }

        if (rayVisualizer == null)
        {
            rayVisualizer = gameObject.AddComponent<LineRenderer>();
            rayVisualizer.startWidth = 0.05f;
            rayVisualizer.endWidth = 0.05f;
            rayVisualizer.positionCount = 2;
            rayVisualizer.material = new Material(newShader);
            rayVisualizer.material.color = Color.cyan;
        }
    }
    private void Update()
    {
        laserPointer();
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            SelectWithRaycast();
        }
    }

    private void SelectWithRaycast()
    {
        Debug.LogError("pressed");
        GameObject rightController = GameObject.Find("RightController");
        GameObject controllerPosition = rightController.transform.Find("OVRControllerVisual").gameObject;
        Vector3 rayOrigin = controllerPosition.transform.position; // Controller position
        Vector3 rayDirection = controllerPosition.transform.forward;
        rayVisualizer.material.color = Color.red;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, selectionDistance, interactableLayer))
        {

            var celestialObject = hit.collider.gameObject.GetComponent<CelestialObject>();
            Debug.Log($"Selected: {celestialObject.name}, {celestialObject.metadata}");
            rayVisualizer.SetPosition(0, rayOrigin);
            rayVisualizer.SetPosition(1, hit.point);
        }
        else
        {
            rayVisualizer.SetPosition(0, rayOrigin);
            rayVisualizer.SetPosition(1, rayOrigin + rayDirection * selectionDistance);
        }
    }

    private void laserPointer()
    {
        GameObject rightController = GameObject.Find("RightController");
        GameObject controllerPosition = rightController.transform.Find("OVRControllerVisual").gameObject;
        Vector3 rayOrigin = controllerPosition.transform.position;
        Vector3 rayDirection = controllerPosition.transform.forward;

        // Perform raycast
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, selectionDistance))
        {
            // Set the laser to hit the object
            rayVisualizer.SetPosition(0, rayOrigin);
            rayVisualizer.SetPosition(1, hit.point);
        }
        else
        {
            // Set the laser to its maximum distance
            rayVisualizer.SetPosition(0, rayOrigin);
            rayVisualizer.SetPosition(1, rayOrigin + rayDirection * selectionDistance);
        }
    }
}
