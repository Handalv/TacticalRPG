using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public GameObject graphic;

    void Start()
    {
        if (graphic == null)
        {
            Debug.Log(gameObject.name + " graphic in null by default");
            graphic = this.gameObject;
        }
    }
}
