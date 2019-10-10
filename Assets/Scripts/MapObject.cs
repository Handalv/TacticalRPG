using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    //We can keep X and Z of this here to avoid of using ConvertWorldCoordToTile

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
