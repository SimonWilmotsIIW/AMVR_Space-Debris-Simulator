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
        GetComponent<Outline>().enabled = true;
        Debug.Log($"Selected: {objectName}");
    }

    public void OnDeselect()
    {
        GetComponent<Outline>().enabled = false;
        Debug.Log($"Deselected: {objectName}");
    }
}
