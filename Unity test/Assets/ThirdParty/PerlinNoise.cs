using UnityEngine;

public static class PerlinNoise
{
    public static float[,] GenerateNoiseMap(int size)
    {

        float[,] noiseMap = new float[size, size];
        float xOffset = Random.Range(-10000f, 10000f);
        float zOffset = Random.Range(-10000f, 10000f);
        float scale = 0.1f;
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                noiseMap[x, y] = Mathf.PerlinNoise(
                    x * scale + xOffset, y * scale + zOffset);

        return noiseMap;
    }
}

