using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.PackageManager;

public class QTE : MonoBehaviour
{

    // INSTRUCTIONS/INFO:
    // - This script implements a Quick Time Event System.
    // - Attach this script to the GameManager
    // - PlayQTE() is used to run a QTE Coroutine from other scripts.

    // Reminder to fill the references in the Unity Editor
    [SerializeField] private GameObject QTEBox; // Drag and drop the "QTE" GameObject in the QTE prefab 
    [SerializeField] private RawImage QTEInnerBox; // Drag and drop the "QTEInner" GameObject in the QTE prefab
    [SerializeField] private Text QTEText; // Drag and drop the "QTEText" GameObject in the QTE prefab


    private enum QTEState
    {
        StartInactive, // 
        Active, //
        Success, //
        Fail, //
        Finished //
    }

    private QTEState currentState = QTEState.StartInactive; //
    private float timer; //
    private Slider timerSlider; //

    private bool isRunning = false; // Prevents multiple QTE's from running at the same time

    public event Action OnQTESuccess; //
    public event Action OnQTEFail; //

    // *** Remove this when Initialize() is correctly implemented in the GameManager script (in Start()). ***
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        timerSlider = GameObject.FindWithTag("Slider").GetComponent<Slider>(); // Add "Slider" tag to the Slider GameObject (located in QTE GameObject [in Canvas])
        QTEBox.SetActive(false);
    }

    // Runs the QTE coroutine and utilizes the isRunning bool to prevent multiple QTE's from running at the same time.
    public void PlayQTE(KeyCode key, float timeLimit)
    {
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(RunQTE(key, timeLimit));
        }
    }

    private IEnumerator RunQTE(KeyCode key, float timeLimit)
    {
        bool sequenceFinished = false;
        
        while (!sequenceFinished)
        {
            switch (currentState)
            {
                case QTEState.StartInactive:
                    BeginQTE(key, timeLimit);
                    break;

                case QTEState.Active:
                    UpdateTimer(timeLimit);
                    CheckInput(key);
                    break;

                case QTEState.Success:
                case QTEState.Fail:
                    break;

                case QTEState.Finished:
                    FinishedQTE();
                    sequenceFinished = true;
                    break;
            }

            yield return null; // The coroutine stops here and resumes in the next frame (so the game remains responsive while QTE is still active).
        }
        isRunning = false; // Sets is Running to false so the next coroutine can be ran.
    }

    private void BeginQTE(KeyCode key, float timeLimit)
    {
        currentState = QTEState.Active;
        timer = timeLimit;
        QTEInnerBox.color = Color.gray;
        QTEBox.SetActive(true);



        QTEText.text = $"[{key}]";

        if (timerSlider != null)
        {
            timerSlider.value = 1f;
        }
    }

    private void UpdateTimer(float timeLimit)
    {
        timer -= Time.deltaTime;

        if (timerSlider != null)
        {
            timerSlider.value = Mathf.Clamp01(timer / timeLimit);
        }

        if (timer <= 0f)
        {
            FailQTE();
        }
    }

    private void CheckInput(KeyCode key)
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(key))
            {
                SuccessQTE();
            }
        }
    }

    private void SuccessQTE()
    {
        currentState = QTEState.Success;
        QTEText.text = "";
        OnQTESuccess?.Invoke();
        QTEInnerBox.color = Color.green;
        StartCoroutine(ResetAfterDelay());
    }

    private void FailQTE()
    {
        currentState = QTEState.Fail;
        QTEText.text = "";
        OnQTEFail?.Invoke();
        QTEInnerBox.color = Color.red;
        StartCoroutine(ResetAfterDelay());
    }

    private void FinishedQTE()
    {
        QTEBox.SetActive(false);

        currentState = QTEState.StartInactive;
        OnQTESuccess = null;
        OnQTEFail = null;
    }

    // Delays the QTE from being set inactive for a set amount of time (0.75 right now)
    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(0.75f);
        currentState = QTEState.Finished;
    }
}