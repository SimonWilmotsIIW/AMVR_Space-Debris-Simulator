using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour, ISelectable
{
    public string objectName;
    public string metadata;
    public float sizeMultiplier = 1.0f;
    public float rotationSpeed = 1.0f;

    public Renderer objectRenderer;
    private Color originalColor;
    private Outline outline;

    private void Start()
    {
        transform.localScale = Vector3.one * sizeMultiplier;
        if (objectRenderer == null) objectRenderer = GetComponent<Renderer>();
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = GetComponentInChildren<Outline>();
        }
        originalColor = objectRenderer.material.color;
    }

    public void Update() 
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }

    public void OnHover() {
        
        outline.OutlineColor = Color.cyan;
        outline.enabled = true;
        Debug.Log($"Hover: {objectName}");
    }
    public void OnUnhover() {
        
        outline.enabled = false;
        outline.OutlineColor = Color.magenta;
    }

    public void OnSelect()
    {
        outline.OutlineColor = Color.magenta;
        outline.enabled = true;
        Debug.Log($"Selected: {objectName}");

        if (this == GameController.Instance.GetTargetObject()) {
            GameController.Instance.StopTimer();
        }
    }

    public void OnDeselect()
    {
        outline.enabled = false;
        Debug.Log($"Deselected: {objectName}");
    }

    public string GetName() {
        return objectName;
    }
}
