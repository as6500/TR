using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

[ExecuteInEditMode]
public class TriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject lightReference;
    private Light2D light;

    private ParticleSystem ps;

    private int numEnter = 0;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    private List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        light = lightReference.GetComponent<Light2D>();
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(p.startColor.r, p.startColor.g, p.startColor.b, GetAlpha(p));
            enter[i] = p;
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(p.startColor.r, p.startColor.g, p.startColor.b, 0);
            exit[i] = p;
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }

    private void Update()
    {
        if (numEnter != 0)
        {
            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = new Color32(p.startColor.r, p.startColor.g, p.startColor.b, GetAlpha(p));
                enter[i] = p;
            }
        }
    }

    private byte GetAlpha(ParticleSystem.Particle particle)
    {
        Vector3 particleRelativePosition = particle.position - lightReference.transform.position;

        Vector3 normParticlePosition = particleRelativePosition.normalized;
        Vector3 normLightPosition = lightReference.transform.position.normalized;

        float angle = Mathf.Acos(normLightPosition.magnitude/normParticlePosition.magnitude) * Mathf.Rad2Deg;

        float a = (Mathf.InverseLerp(light.pointLightOuterAngle, 0, angle) * 255);

        byte alpha = (byte) a;

        return alpha;
    }
}

