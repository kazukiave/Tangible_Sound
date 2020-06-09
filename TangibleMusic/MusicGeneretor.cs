using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicGeneretor : MonoBehaviour
{
    Receiver receiver;

   // [Range(0,2000)]
    public double frequency = 440;
   // [Range(0.00f,1)]
    public double gain = 0.05;

    private double increment;
    private double phase;
    private double sampling_frequency = 48000;

    private void Start()
    {
        receiver = GetComponent<Receiver>();
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        frequency = (double)receiver.sensorVal;
        if(frequency > 100)
        { frequency = 0; }
        frequency *= 10;

        increment = frequency * 2 * Math.PI / sampling_frequency;

        for (var i = 0; i < data.Length; i = i + channels)
        {
            phase = phase + increment;
            data[i] = (float)(gain * Math.Sin(phase));
            if (channels == 2) data[i + 1] = data[i];
            if (phase > 2 * Math.PI) phase = 0;
        }
    }
}
