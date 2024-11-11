using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    [SerializeField] private float timer = 0;
    [SerializeField] private float intervaloMin = 5;
    [SerializeField] private float intervaloMax = 9;
    [SerializeField] private float valorSeleccionado = 0;
    [SerializeField] private AudioSource Source;
    [SerializeField] private AudioClip[] audioClips;



    // Start is called before the first frame update
    void Start()
    {
        valorSeleccionado = Random.Range(intervaloMin, intervaloMax);
        Source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>=valorSeleccionado)
        {
            grito();
        }
    }

    void grito()
    {
        valorSeleccionado = Random.Range(intervaloMin, intervaloMax);
        timer = 0;
        Source.clip = audioClips[Random.Range(0, audioClips.Length-1)];
        Source.Play();
    }
}
