using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask selectableLayer;

    private ISelectable currentSelection;

    private void Update()
    {
        HandleSelection();
    }

    private void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0)) // left
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectableLayer))
            {
                ISelectable selectable = hit.collider.GetComponent<ISelectable>();
                if (selectable != null)
                {
                    //deselection
                    if (currentSelection != null && currentSelection != selectable)
                    {
                        currentSelection.OnDeselect();
                    }

                    currentSelection = selectable;
                    currentSelection.OnSelect();
                }
            }
            else if (currentSelection != null)
            {
                currentSelection.OnDeselect();
                currentSelection = null;
            }
        }
    }
}
