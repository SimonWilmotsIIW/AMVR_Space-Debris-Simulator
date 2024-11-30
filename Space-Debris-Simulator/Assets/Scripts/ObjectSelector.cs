using UnityEngine;

public class VRObjectSelector : MonoBehaviour
{
    private Camera vrCamera;
    public LayerMask interactableLayer;
    public float selectionDistance = 100f;

    public LineRenderer rayVisualizer; 

    private void Start()
    {
        vrCamera = gameObject.GetComponent<Camera>();

        if (rayVisualizer == null)
        {
            rayVisualizer = gameObject.AddComponent<LineRenderer>();
            rayVisualizer.startWidth = 0.05f;
            rayVisualizer.endWidth = 0.05f;
            rayVisualizer.positionCount = 2;
            rayVisualizer.material = new Material(Shader.Find("Sprites/Default"));
            rayVisualizer.startColor = Color.red;
            rayVisualizer.endColor = Color.red;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SelectWithRaycast();
        }
    }

    private void SelectWithRaycast()
    {
        Ray ray = vrCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, selectionDistance, interactableLayer))
        {

            var celestialObject = hit.collider.gameObject.GetComponent<CelestialObject>();
            Debug.Log($"Selected: {celestialObject.name}, {celestialObject.metadata}");
            rayVisualizer.SetPosition(0, ray.origin);
            rayVisualizer.SetPosition(1, hit.point);
        }
        else
        {
            rayVisualizer.SetPosition(0, ray.origin);
            rayVisualizer.SetPosition(1, ray.origin + ray.direction * selectionDistance);
        }
    }
}
