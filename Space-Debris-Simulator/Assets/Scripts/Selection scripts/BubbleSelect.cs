using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSelect : MonoBehaviour
{
    private Transform player;
    public float selectionRadius = 5f;
    public LayerMask selectableLayer;

    private ISelectable currentSelection;
    private LineRenderer lineRenderer;

    private void Start()
    {
        player = transform;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = true;
    }

    private void Update()
    {
        HandleProximitySelection();
        UpdateVisualization();
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

    private void UpdateVisualization()
    {
        if (currentSelection != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, (currentSelection as MonoBehaviour).transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void OnDrawGizmos()
{
    if (player != null)
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(player.position, selectionRadius);

        // If there's a current selection, mark it
        if (currentSelection != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(player.position, (currentSelection as MonoBehaviour).transform.position);
            Gizmos.DrawWireSphere((currentSelection as MonoBehaviour).transform.position, 0.5f);
        }
    }
}
}
