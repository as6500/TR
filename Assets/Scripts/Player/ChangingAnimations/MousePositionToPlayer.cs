using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MousePositionToPlayer : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private int nullDistance;

    [SerializeField] private float currentAngle;

    [SerializeField] private AudioManager audioManager;

    private float tempAngle;

    private void FixedUpdate()
    {
        if(audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        SetAngle();
        AngleChanged();
    }

    private void AngleChanged()
    {
        if(currentAngle != tempAngle)
        {
            tempAngle = currentAngle;
            TriggerAnimation();
        }
    }

    private void SetAngle()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        if (mousePos.magnitude < nullDistance)
            return;

        currentAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (currentAngle < 45f && currentAngle > -45f)
        {
            currentAngle = 0;
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (currentAngle > 45f && currentAngle < 135f)
        {
            currentAngle = 90;
        }
        else if (currentAngle < -45f && currentAngle > -135f)
        {
            currentAngle = -90;
        }
        else
        {
            currentAngle = 360;
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void TriggerAnimation()
    {
        if (currentAngle == 0)
        {
            anim.SetTrigger("side");
        }
        else if (currentAngle == 90)
        {
            anim.SetTrigger("up");
        }
        else if (currentAngle == -90)
        {
            anim.SetTrigger("down");
        }
        else
        {
            anim.SetTrigger("side");
        }
    }

    public void Step()
    {
        int randomStep = Random.Range(0,2);

        if (SceneManager.GetActiveScene().name == "BunkerOutside" || SceneManager.GetActiveScene().name == "Farm")
        {
            if (randomStep == 0)
            {
                audioManager.stepOneSandFloor.Play();
            }
            else
            {
                audioManager.stepTwoSandFloor.Play();
            }
        }
        else
        {
            if (randomStep == 0)
            {
                audioManager.stepOneSolidFloor.Play();
            }
            else
            {
                audioManager.stepTwoSolidFloor.Play();
            }
        }
        

    }
}
