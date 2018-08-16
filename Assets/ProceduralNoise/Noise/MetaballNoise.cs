using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using ProceduralNoiseProject;
using UnityEngine;

public class MetaballNoise : INoise {
    private Vector4[] balls;
    
    public MetaballNoise(int seed, int minBalls, int maxBalls, float minRadius, float maxRadius, Bounds bounds) {
        UpdateSeed(seed);

        int numBalls = Random.Range(minBalls, maxBalls);
        balls = new Vector4[numBalls];

        for (int i = 0; i < numBalls; ++i) {
            float radius = Random.Range(minRadius, maxRadius);
            float x = Random.Range(bounds.min.x + radius, bounds.max.x - radius);
            float y = Random.Range(bounds.min.y + radius, bounds.max.y - radius);
            float z = Random.Range(bounds.min.z + radius, bounds.max.z - radius);
            
            balls[i] = new Vector4(x, y, z, radius);
        }
    }

    public float Frequency { get; set; }
    public float Amplitude { get; set; }
    public Vector3 Offset { get; set; }
    
    public float Sample1D(float x) {
        return Sample2D(x, 0);
    }

    public float Sample2D(float x, float y) {
        return Sample3D(x, y, 0);
    }

    public float Sample3D(float x, float y, float z) {
        float value = 0;
        foreach (var b in balls) {
            float dx = (b.x - x);
            float dy = (b.y - y);
            float dz = (b.z - z);
            value += b.w * b.w / (dx * dx + dy * dy + dz * dz);
        }

        value -= 1.0f;
        return value;
    }

    public void UpdateSeed(int seed) {
        if (seed != 0) {
            Random.InitState(seed);
        }
    }
}
