using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntrancesAndExits : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private EEntranceType whereToLoadTo;
    [SerializeField] private EBuildings whereToLoadToBuildings;
    private bool changingScenes = false;
    private FadeInAndOutBlackScreen blackScreen;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        blackScreen = GameObject.FindGameObjectWithTag("FadeInAndOutBlackScreen").GetComponent<FadeInAndOutBlackScreen>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        StartCoroutine(GoToScene(sceneName, whereToLoadTo, whereToLoadToBuildings));
    }
    
    public IEnumerator GoToScene(String nameOfScene, EEntranceType place, EBuildings buildings)
    {
        if(!changingScenes)
        {
            changingScenes = true;

            blackScreen.ResetAndStartFade(blackScreen.ChangeBlackScreenOpacityUp());
            yield return new WaitUntil(blackScreen.FadeEnded);

            SceneManager.LoadScene(nameOfScene);

            player.GetComponent<MapChangingInfo>().entranceTypeToFind = place;
            player.GetComponent<MapChangingInfo>().buildingTypeToFind = buildings;

            changingScenes = false;
        }
    }
}
