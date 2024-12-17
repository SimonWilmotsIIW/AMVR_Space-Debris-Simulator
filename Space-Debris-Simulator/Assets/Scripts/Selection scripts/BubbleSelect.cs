using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSelect : MonoBehaviour
{
    private Transform player;
    public float selectionRadius = 5f;
    public LayerMask selectableLayer;

    private ISelectable currentSelection;
    private GameObject sphere;

    private void Start()
    {
        player = transform;

        // Create a sphere in the scene
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = player.position; // Initial position at the player's position
        sphere.transform.localScale = Vector3.one * selectionRadius * 2f; // Scale to match selection radius
        sphere.GetComponent<Collider>().enabled = false; // Disable collider if not needed for interaction

        // Optional: Assign a material to the sphere for visibility
        Renderer sphereRenderer = sphere.GetComponent<Renderer>();
        sphereRenderer.material = new Material(Shader.Find("Standard"));
        sphereRenderer.material.color = new Color(1f, 0f, 0f, 0.3f); // Red with some transparency
    }

    private void Update()
    {
        HandleProximitySelection();
        UpdateSphereProperties();
    }

    private void HandleProximitySelection()
    {
        Collider[] hits = Physics.OverlapSphere(player.position, selectionRadius, selectableLayer);

        if (hits.Length > 0)
        {
            ISelectable closest = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider hit in hits)
            {
                float distance = Vector3.Distance(player.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closest = hit.GetComponent<ISelectable>();
                    closestDistance = distance;
                }
            }

            if (closest != null && closest != currentSelection)
            {
                if (currentSelection != null) currentSelection.OnDeselect();
                currentSelection = closest;
                currentSelection.OnSelect();
            }
        }
        else if (currentSelection != null)
        {
            currentSelection.OnDeselect();
            currentSelection = null;
        }
    }

    private void UpdateSphereProperties()
    {
        // Update sphere's position to follow the player
        sphere.transform.position = player.position;

        // Update sphere's scale to reflect the selection radius
        sphere.transform.localScale = Vector3.one * selectionRadius * 2f;
    }

    private void OnDestroy()
    {
        // Cleanup: Destroy the sphere when this script is destroyed
        if (sphere != null)
        {
            Destroy(sphere);
        }
    }
}
