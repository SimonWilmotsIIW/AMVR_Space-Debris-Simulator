using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour, ISelectable
{
    private CelestialObject linkedObject;

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
        OnHoverEffect();
        linkedObject?.OnHoverEffect();
    }
    public void OnHoverEffect()
    {
        outline.OutlineColor = Color.cyan;
        outline.enabled = true;
        Debug.Log($"Hover: {objectName}");
    }
    public void OnUnhover() {

        OnUnhoverEffect();
        linkedObject?.OnUnhoverEffect();
    }
    public void OnUnhoverEffect()
    {
        outline.enabled = false;
        outline.OutlineColor = Color.magenta;
    }

    public void OnSelect()
    {
        OnSelectEffect();
        linkedObject?.OnSelectEffect();
    }
    public void OnSelectEffect()
    {
        outline.OutlineColor = Color.magenta;
        GetComponent<Outline>().enabled = true;
        Debug.Log($"Selected: {objectName}");


        if (this == GameController.Instance.GetTargetObject())
        {
            GameController.Instance.StopTimer();
        }
    }

    public void OnDeselect()
    {
        OnDeselectEffect();
        linkedObject?.OnDeselectEffect();
    }
    public void OnDeselectEffect()
    {
        GetComponent<Outline>().enabled = false;
        Debug.Log($"Deselected: {objectName}");
    }
    public string GetName() {
        return objectName;
    }

    public void SetLinkedObject(CelestialObject other)
    {
        linkedObject = other;
    }
}
