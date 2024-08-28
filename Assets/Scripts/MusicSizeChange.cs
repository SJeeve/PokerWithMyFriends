using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicSizeChange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource musicSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;
    private float ObjectSize;
    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] samples;
    void Awake()
    {
        samples = new float[sampleDataLength];
        ObjectSize = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Thank you to Chris_Entropy on Unity Discussions for the code to get current "loudness"
        currentUpdateTime += Time.deltaTime;
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
        gameObject.transform.localScale = new Vector3(ObjectSize * (clipLoudness + 1), ObjectSize * (clipLoudness + 1), ObjectSize * (clipLoudness + 1));
        
    }
}
