using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocationOfPlayerInGameMenu : MonoBehaviour
{
    [SerializeField] private Vector2 bunker;
    [SerializeField] private Vector2 outside;
    [SerializeField] private Vector2 road;
    [SerializeField] private Vector2 farm;
    [SerializeField] private Vector2 city;
    
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameObject.GetComponent<RectTransform>().localPosition = bunker;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "BunkerInside")
            gameObject.GetComponent<RectTransform>().localPosition = bunker;
        else if (arg0.name == "BunkerOutside")
            gameObject.GetComponent<RectTransform>().localPosition = outside;
        else if (arg0.name == "Road")
            gameObject.GetComponent<RectTransform>().localPosition = road;
        else if (arg0.name == "Farm")
            gameObject.GetComponent<RectTransform>().localPosition = farm;
        else if (arg0.name == "City")
            gameObject.GetComponent<RectTransform>().localPosition = city;
    }
}
