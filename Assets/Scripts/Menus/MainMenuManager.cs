using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    [Header("API Connection")]
    [SerializeField] private APIRequests APIReq;
    [SerializeField] private CreateConnection createConnection;
    [SerializeField] private CloseConnection closeConnection;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private GameObject settingsView;
    [SerializeField] private GameObject appView;
    [SerializeField] private GameObject codeView;
    [SerializeField] private Text connectionCodeText;

    void Start()
    {
        GoBackMainOptions();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    //Main Menu Functions
    public void GoToSettingsView()
    {
        audioManager.mouseClicking.Play();
        settingsView.SetActive(true);
        appView.SetActive(false);
        mainOptions.SetActive(false);
        codeView.SetActive(false);
    }

    public void GoToAppView()
    {
        audioManager.mouseClicking.Play();
        appView.SetActive(true);
        settingsView.SetActive(false);
        codeView.SetActive(false);
        mainOptions.SetActive(false);
    }

    public void GoToCodeView()
    {
        appView.SetActive(true);
        settingsView.SetActive(false);
        codeView.SetActive(true);
        mainOptions.SetActive(false);
    }

    public void GoBackMainOptions()
    {
        audioManager.mouseClicking.Play();
        settingsView.SetActive(false);
        codeView.SetActive(false);
        appView.SetActive(false);
        mainOptions.SetActive(true);
    }

    public void GoBackAppView()
    {
        settingsView.SetActive(false);
        codeView.SetActive(false);
        appView.SetActive(true);
        mainOptions.SetActive(false);
    }

    public void StartGame()
    {
        audioManager.mouseClicking.Play();
        SceneManager.LoadScene("BunkerInside");
    }

    public void GetConnectionCode()
    {
        audioManager.mouseClicking.Play();
        createConnection.CreateNewConnection(ShowCode, GoBackAppView);
    }

    private void ShowCode()
    {
        audioManager.mouseClicking.Play();
        UnJasonedData data = APIReq.response;
        GoToCodeView();
        connectionCodeText.text = data.unity_connection_code.ToString();
    }

    public void CancelSearch()
    {
        closeConnection.CloseExistingConnection();
        GoToAppView();
    }

    public void LeaveGame()
    {
        audioManager.mouseClicking.Play();
#if UNITY_EDITOR
        closeConnection.CloseExistingConnection();
        UnityEditor.EditorApplication.isPlaying = false;
#else
        closeConnection.CloseExistingConnection();
        Application.Quit();
#endif
    }
}
