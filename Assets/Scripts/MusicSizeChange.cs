using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicSizeChange : MonoBehaviour
{
    //Stop being nosy Will
    public AudioSource musicSource;
    public float updateStep = 0.1f;
    //Number of samples to get 
    //1024 samples is ~80 ms
    //Depends on hz
    public int sampleDataLength = 1024;
    //Stores the initial scale of an object's x y and z scales
    //Storing each scale not needed for this project, but might be useful for future projects
    private float[] baseSize = new float[3];
    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] samples;
    void Awake()
    {
        musicSource = gameObject.GetComponent<AudioSource>();
        samples = new float[sampleDataLength];
        baseSize[0] = gameObject.transform.localScale.x;
        baseSize[1] = gameObject.transform.localScale.y;
        baseSize[2] = gameObject.transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        //Updates every tenth of a second
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            musicSource.clip.GetData(samples, musicSource.timeSamples);
            clipLoudness = 0f;
            foreach (float sample in samples)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength;
        }
        gameObject.transform.localScale = new Vector3(baseSize[0] * (clipLoudness + 1), baseSize[1] * (clipLoudness + 1), baseSize[2] * (clipLoudness + 1));
        //This code gets the current "loudness" of a music clip by getting a number of samples equivalent to sampleDataLength then averaging all of the samples together 
        //It then sets the scale of the object to the starting scale multiplied by the average loudness + 1
    }
}
