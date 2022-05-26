using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefabMapping[] mappings;
    public float offset = 5;

    public void Generate()
    {
        for(int x=0; x < map.width; x++)
        {
            for(int y=0; y < map.height; y++)
            {
                Color color = map.GetPixel(x, y);

                foreach(ColorToPrefabMapping m in mappings)
                {
                    if(m.color == color)
                    {
                        Vector3 position = new Vector3(x, 0, y) * offset;

                        Instantiate(m.prefab, position, Quaternion.identity, transform);
                    }
                }
            }
        }
    }
}
