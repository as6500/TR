using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantHumanAttack : MonoBehaviour
{
    [SerializeField] private MutantHumanState state;
    private bool attacking;


    void Start()
    {
        state = GetComponent<MutantHumanState>();
        attacking = false; 
    }

    private void Update()
    {
        if (attacking) 
        {
            ActivOrDeactivAttackState(); //updateAttackState
        }
    }

    public void ActivOrDeactivAttackState()
    {
        if (state.GetCurrentState() == State.Attack)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
    }
}
