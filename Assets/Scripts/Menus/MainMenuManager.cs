using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("API Connection")]
    [SerializeField] private APIRequests APIReq;
    [SerializeField] private CreateConnection createConnection;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private GameObject settingsView;
    [SerializeField] private GameObject appView;
    [SerializeField] private GameObject codeView;
    [SerializeField] private Text connectionCodeText;

    void Start()
    {
        GoBackMainOptions();
    }

    //Main Menu Functions
    public void GoToSettingsView()
    {
        settingsView.SetActive(true);
        appView.SetActive(false);
        mainOptions.SetActive(false);
        codeView.SetActive(false);
    }

    public void GoToAppView()
    {
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
        SceneManager.LoadScene("BunkerInside");
    }

    public void GetConnectionCode()
    {
        createConnection.CreateNewConnection(ShowCode, GoBackAppView);
    }

    private void ShowCode()
    {
        UnJasonedData data = APIReq.response;
        GoToCodeView();
        connectionCodeText.text = data.unity_connection_code.ToString();
    }

    public void LeaveGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
