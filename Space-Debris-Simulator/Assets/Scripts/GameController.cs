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
    private CelestialObject targetObject;

    private float elapsedTime = 0f;
    private bool isTimerRunning = false;

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
        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleOrbitVisibility();
        } else if (Input.GetKeyDown(KeyCode.Space) && enableTimer) {
            if (isTimerRunning) {
                StopTimer();
            } else {
                StartTimer();
                ResetPlayerPosition();
                SelectRandomCelestialObject();
            }
        }

        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
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
    }

    public void StopTimer()
    {
        isTimerRunning = false;
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
        player.transform.position = new Vector3(0f, 0f, 0f);
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

    }
}
