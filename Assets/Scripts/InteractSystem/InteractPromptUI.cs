using TMPro;
using UnityEngine;

/// <summary>
/// Manages Prompts. Can display text dynamically and rotates to always face the camera. 
/// Also provides methods for closing and opening the prompt with custom text.
/// </summary>
/// <remarks>
/// For the prompt to display text, the IInteractable component must have the "Prompt" property initalized.
/// </remarks>
public class InteractPromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textPrompt;
    [SerializeField] private GameObject _uiPanel;
    private Camera _mainCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCam = Camera.main;
        ClosePrompt();
    }

    /// <summary>
    /// Handles the pop-up's rotation so it is always facing the camera.
    /// </summary>
    private void LateUpdate()
    {
        Quaternion rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void OpenPrompt(string newText)
    {
        _textPrompt.text = newText;
        _uiPanel.SetActive(true);
    }

    public void ClosePrompt()
    {
        _uiPanel.SetActive(false);
        _textPrompt.text = "";
    }
}
