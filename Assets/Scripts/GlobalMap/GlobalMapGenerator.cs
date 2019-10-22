using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapGenerator : MonoBehaviour
{
    public GlobalMap map;

    void Awake()
    {
        if (map == null)
        {
            Debug.Log("GlobalMap in MapGenerator is null by default");
            map = GetComponent<GlobalMap>();
        }
    }

    
}
