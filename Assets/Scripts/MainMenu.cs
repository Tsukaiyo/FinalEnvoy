using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private VisualElement _root;

    private Button _newGameButton;
    private Button _continueButton;
    private Button _optionsButton;
    private Button _creditsButton;
    private Button _quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Get Root visual element (This would be the container visual element)
        _root = GetComponent<UIDocument>().rootVisualElement;

        // Get references to Button elements
        _newGameButton = _root.Q<Button>("NewGameButton");
        _continueButton = _root.Q<Button>("ContinueButton");
        _optionsButton = _root.Q<Button>("OptionsButton");
        _creditsButton = _root.Q<Button>("CreditsButton");
        _quitButton = _root.Q<Button>("QuitButton");

    }

    /// <summary>
    /// Connect button "clicked" events to their respective funcitons
    /// </summary>
    void OnEnable()
    {
        _newGameButton.clicked += OnNewGameButtonPressed;
        _continueButton.clicked += OnContinueButtonPressed;
        _optionsButton.clicked += OnOptionsButtonPressed;
        _creditsButton.clicked += OnCreditsButtonPressed;
        _quitButton.clicked += OnQuitButtonPressed;
    }


    /// <summary>
    /// Disconnect button "clicked" events when UI disabled
    /// </summary>
    void OnDisable()
    {
        _newGameButton.clicked -= OnNewGameButtonPressed;
        _continueButton.clicked -= OnContinueButtonPressed;
        _optionsButton.clicked -= OnOptionsButtonPressed;
        _creditsButton.clicked -= OnCreditsButtonPressed;
        _quitButton.clicked -= OnQuitButtonPressed;
    }

    /// <summary>
    /// Load Main Game
    /// </summary>
    private void OnNewGameButtonPressed() 
    {
        SceneManager.LoadScene("Level1Day0_FullMap");
        // Debug.Log("New Game Button Pressed");
    }

    private void OnContinueButtonPressed() 
    {
        Debug.Log("Continue Button Pressed!");
    }

    private void OnOptionsButtonPressed() 
    {
        Debug.Log("Continue Button Pressed!");
    }

    /// <summary>
    /// Load Credits Screen
    /// </summary>
    private void OnCreditsButtonPressed() 
    {
        // Debug.Log("Credits Button Pressed!");
        SceneManager.LoadScene("Credits");
    }

    /// <summary>
    /// Exits the Game and closes the editor.
    /// </summary>
    private void OnQuitButtonPressed() 
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
