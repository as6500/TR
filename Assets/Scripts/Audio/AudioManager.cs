using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource MainMenuMusic;
    [SerializeField] private AudioSource GamePlayMusic;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource buttonSelecting;
    [SerializeField] private AudioSource mouseClicking;
    [SerializeField] private AudioSource deathSound;

    [Header("Player")]
    [Header("   Movement")]
    [SerializeField] private AudioSource stepOneSolidFloor;
    [SerializeField] private AudioSource stepTwoSolidFloor;
    [SerializeField] private AudioSource stepOneSandFloor;
    [SerializeField] private AudioSource stepTwoSandFloor;
    
    [Header("   Weapons")]
    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource swingOne;
    [SerializeField] private AudioSource swingTwo;

    [Header("   Health")]
    [SerializeField] private AudioSource loseHealthOne;
    [SerializeField] private AudioSource loseHealthTwo;
    [SerializeField] private AudioSource loseHealthThree;
    [SerializeField] private AudioSource gainHealth;

    [Header("   FlashLight")]
    [SerializeField] private AudioSource flashlightOn;
    [SerializeField] private AudioSource flashlightOff;

    [Header("Enemies")]
    [Header("   MutantTree")]
    [SerializeField] private AudioSource sapOne;
    [SerializeField] private AudioSource sapTwo;
    [SerializeField] private AudioSource sapThree;

    [Header("   Worm")]
    [SerializeField] private AudioSource drag;
}
