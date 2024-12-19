using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour, ISelectable
{
    public CelestialObject linkedObject;

    public string objectName;
    public string metadata;
    public float sizeMultiplier = 1.0f;
    public float rotationSpeed = 1.0f;

    public Renderer objectRenderer;
    private Color originalColor;
    private Outline outline;

    private bool isSelected;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = GetComponentInChildren<Outline>();
        }
    }

    private void Start()
    {
        transform.localScale = Vector3.one * sizeMultiplier;
        if (objectRenderer == null) objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        isSelected = false;
    }

    public void Update() 
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }

    public void OnHover() {
        Debug.Log("Hover");
        Debug.Log(isSelected);
        if(!isSelected){
            OnHoverEffect();
            linkedObject?.OnHoverEffect();
        }
    }
    public void OnHoverEffect()
    {
        outline.OutlineColor = Color.cyan;
        outline.enabled = true;
        Debug.Log($"Hover: {objectName}");
    }
    public void OnUnhover() {
        if(!isSelected){
            OnUnhoverEffect();
            linkedObject?.OnUnhoverEffect();
        }
    }
    public void OnUnhoverEffect()
    {
        outline.enabled = false;
        outline.OutlineColor = Color.magenta;
    }

    public void OnSelect()
    {
        if(!isSelected){
            isSelected=true;
        }
        OnSelectEffect();
        linkedObject?.OnSelectEffect();
    }
    public void OnSelectEffect()
    {
        outline.OutlineColor = Color.magenta;
        outline.enabled = true;
        Debug.Log($"Selected: {objectName}");

        if (this == GameController.Instance.GetTargetObject())
        {
            GameController.Instance.StopTimer();
        }
    }

    public void OnDeselect()
    {
        //isSelected=false;
        OnDeselectEffect();
        linkedObject?.OnDeselectEffect();
    }
    public void OnDeselectEffect()
    {
        isSelected = false;
        outline.enabled = false;
    }
    public string GetName() {
        return objectName;
    }

    public void SetLinkedObject(CelestialObject other)
    {
        linkedObject = other;
    }

    public void DestroyLinkedObject()
    {
        linkedObject = null;
    }
}
