using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MousePositionToPlayer : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private int nullDistance;

    [SerializeField] private float currentAngle;
    [SerializeField] private float currentWholeAngle;
    private float tempAngle;

    private void FixedUpdate()
    {
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

        currentWholeAngle = currentAngle;

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
}
