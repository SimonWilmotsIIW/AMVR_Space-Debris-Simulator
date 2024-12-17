using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionOfObject : MonoBehaviour
{
    public GameObject ObjectTextPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectWithRaycast()
    {
        CelestialObject celestialObject = gameObject.GetComponent<CelestialObject>();
        celestialObject.OnSelect();

        ObjectTextPanel.SetActive(true);
        GameObject nameObject = ObjectTextPanel.transform.Find("ObjectName").gameObject;
        GameObject infoObject = ObjectTextPanel.transform.Find("ObjectInfo").gameObject;

        TMP_Text nameText = nameObject.GetComponent<TMP_Text>();
        TMP_Text infoText = infoObject.GetComponent<TMP_Text>();

        nameText.text = celestialObject.GetName();
        infoText.text = celestialObject.metadata;

        Debug.Log($"Selected: {celestialObject.name}, {celestialObject.metadata}");
    }
}
