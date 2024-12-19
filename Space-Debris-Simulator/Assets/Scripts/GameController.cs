using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public CelestialObject[] celestialObjects;
    public GameObject player;

    public bool enableTimer;

    public TMP_Text findText;
    public TMP_Text timerText;
    public CelestialObject targetObject;

    private float elapsedTime = 0f;
    private bool isTimerRunning = false;
    private int amountOfClicksWithoutObjectHit = 0;

    public GameObject ClicksPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        celestialObjects = FindObjectsOfType<CelestialObject>();
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) || Input.GetKeyDown(KeyCode.O))
        {
            ToggleOrbitVisibility();
        } else if (Input.GetKeyDown(KeyCode.Space) && enableTimer) {
            if (isTimerRunning) {
                amountOfClicksWithoutObjectHit++;
                StopTimer();
            } else {
                StartTimer();
                ResetPlayerPosition();
                SelectRandomCelestialObject();
                amountOfClicksWithoutObjectHit = 0;
            }
        }

        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            amountOfClicksWithoutObjectHit++;
        }
    }

    public void AmountOfClicksCorrection()
    {
        amountOfClicksWithoutObjectHit -= 1;
    }

    private void ToggleOrbitVisibility()
    {
        foreach (CelestialObject obj in celestialObjects)
        {
            //GameObject obj = celestialObj.gameObject;
            Debug.Log(obj.GetName());
            if (obj.GetComponent<Orbit>() != null)
            {
                var orbit = obj.GetComponent<Orbit>();
                if (orbit != null)
                {
                    orbit.ToggleOrbitVisibility(!orbit.GetOrbitRenderer().enabled);
                }
            }
            
        }
    }

    public CelestialObject GetTargetObject() {
        return targetObject;
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true;

        GameObject clickObject = ClicksPanel.transform.Find("Clicks").gameObject;
        TMP_Text clicksText = clickObject.GetComponent<TMP_Text>();
        clicksText.text = "Amount of  clicks visible after test";
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.LogError(amountOfClicksWithoutObjectHit);

        GameObject clickObject = ClicksPanel.transform.Find("Clicks").gameObject;
        TMP_Text clicksText = clickObject.GetComponent<TMP_Text>();
        clicksText.text = "Amount of  clicks: " + amountOfClicksWithoutObjectHit;

        Debug.Log($"Timer Stopped! Final Time: {elapsedTime:F2} seconds");
    }
    private void UpdateTimerText()
    {
        timerText.text = $"Time: {elapsedTime:F2} s";
    }
    void SelectRandomCelestialObject()
    {
        if (celestialObjects.Length > 0)
        {
            targetObject = celestialObjects[Random.Range(0, celestialObjects.Length)];
            findText.text = $"Find: {targetObject.GetName()}";
            Debug.Log($"Find: {targetObject.GetName()}");
        }
    }

    private void ResetPlayerPosition() {
        player.transform.position = new Vector3(0f, 0f, -5f);
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
