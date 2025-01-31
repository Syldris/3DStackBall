using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDeactivate : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if(!particleSystem.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
