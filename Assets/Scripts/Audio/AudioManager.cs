using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public AudioSource mainMenuMusic { get; set; }
    [field: SerializeField] public AudioSource gamePlayMusic { get; set; }

    [field: Header("SoundEffects")]
    [field: SerializeField] public AudioSource buttonSelecting { get; set; }
    [field: SerializeField] public AudioSource mouseClicking { get; set; }
    [field: SerializeField] public AudioSource deathSound { get; set; }

    [field:Header("Player")]
    [field: Header("   Movement")]
    [field: SerializeField] public AudioSource stepOneSolidFloor { get; set; }
    [field: SerializeField] public AudioSource stepTwoSolidFloor { get; set; }
    [field: SerializeField] public AudioSource stepOneSandFloor { get; set; }
    [field: SerializeField] public AudioSource stepTwoSandFloor { get; set; }

    [field: Header("   Weapons")]
    [field: SerializeField] public AudioSource shoot { get; set; }
    [field: SerializeField] public AudioSource swingOne { get; set; }
    [field: SerializeField] public AudioSource swingTwo { get; set; }

    [field: Header("   Health")]
    [field: SerializeField] public AudioSource loseHealthOne { get; set; }
    [field: SerializeField] public AudioSource loseHealthTwo { get; set; }
    [field: SerializeField] public AudioSource loseHealthThree { get; set; }
    [field: SerializeField] public AudioSource gainHealth { get; set; }

    [field: Header("   FlashLight")]
    [field: SerializeField] public AudioSource flashlightOn { get; set; }
    [field: SerializeField] public AudioSource flashlightOff { get; set; }

    [field: Header("Enemies")]
    [field: Header("   MutantTree")]
    [field: SerializeField] public AudioSource sapOne { get; set; }
    [field: SerializeField] public AudioSource sapTwo { get; set; }
    [field: SerializeField] public AudioSource sapThree { get; set; }

    [field: Header("   Worm")]
    [field: SerializeField] public AudioSource drag { get; set; }

    private string currentSceneName;

    //DontDestroyOnLoad

    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentSceneName = SceneManager.GetActiveScene().name;
        mainMenuMusic.Play();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "Main Menu")
        {
            currentSceneName = arg0.name;
            gamePlayMusic.Stop();
            mainMenuMusic.Play();
        }
        else if (currentSceneName == "Main Menu" && arg0.name != "Main Menu")
        {
            currentSceneName = arg0.name;
            gamePlayMusic.Play();
            mainMenuMusic.Stop();
        }
        else if (currentSceneName != "Main Menu" && arg0.name != "Main Menu")
        {
            currentSceneName = arg0.name;
        }
    }
}
