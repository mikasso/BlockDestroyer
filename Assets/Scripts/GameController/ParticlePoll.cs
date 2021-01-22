using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoll : MonoBehaviour
{

    private GameObject[] particlesPoll;
    public GameObject particleObject;
    public int pollSize;
    private int lastChoosen = 0;
    public void Awake()
    {
        particlesPoll = new GameObject[pollSize];
        for (int i = 0; i < pollSize; i++)
        {
            particlesPoll[i] = Instantiate(particleObject, particleObject.transform.position, Quaternion.identity);
            ParticleSystem.MainModule main = particlesPoll[i].GetComponentInChildren<ParticleSystem>().main;
            main.startColor = GetComponent<ColorsPoll>().GetRandomColor();
        }
    }

    public void PlayOneShot(Vector3 position)
    {
        lastChoosen += 1;
        lastChoosen %= pollSize;
        var particles = particlesPoll[lastChoosen];
        particles.transform.position = position;
        particles.GetComponent<ParticleSystem>().Play();
    }
}
