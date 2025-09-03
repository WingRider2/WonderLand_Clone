using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        Time.timeScale = 1f;
        settingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerInput != null)
        {
            if (!playerInput.enabled)
                playerInput.enabled = true;
            playerInput.SwitchCurrentActionMap("Player");
        }
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!settingsPanel.activeSelf)
                OpenSettingsPanel();
            else
                CloseSettingsPanel();
        }
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerInput == null)
            playerInput = FindObjectOfType<PlayerInput>();

        if (playerInput != null)
        {
            if (!playerInput.enabled)
                playerInput.enabled = true;
            playerInput.SwitchCurrentActionMap("UI");
        }

        Time.timeScale = 0f;
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerInput == null)
            playerInput = FindObjectOfType<PlayerInput>();

        if (playerInput != null)
        {
            if (!playerInput.enabled)
                playerInput.enabled = true;
            playerInput.SwitchCurrentActionMap("Player");
        }

        Time.timeScale = 1f;
    }
}
