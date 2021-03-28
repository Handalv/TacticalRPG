
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GlobalMap map;
    public Unit playerUnit;

    [SerializeField]
    private Camera camera;
    //TODO Need to make coord dependence on mapsize.
    // Can wait to make it until player get opportunity to change mapsize before generating ;)
    [SerializeField]
    private float cameraXMin = 8.55f;
    [SerializeField]
    private float cameraXMax = 41.6f;
    [SerializeField]
    private float cameraZMin = -1.5f;
    [SerializeField]
    private float cameraZMax = 25.8f;

    void Start()
    {
        map = GlobalMap.instance;
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    void Update()
    {
        //TODO Check overlay pointer
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, ray.direction);
        RaycastHit[] hits;
        if (!GlobalMap.instance.GAMEPAUSED)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hits = Physics.RaycastAll(ray);
                foreach (RaycastHit hit in hits)
                {
                    Tile hitTile = hit.collider.gameObject.GetComponent<Tile>();
                    if (hitTile != null)
                    {
                        playerUnit.SetDestanation(map.GeneratePathTo(hitTile.tileX, hitTile.tileZ, playerUnit.tileX, playerUnit.tileZ));
                    }
                }
            }
        }

        //TEST Remove warfog
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (map.globalWarfogEnabled)
            {
                foreach (Tile tile in map.tiles)
                    tile.WarFogEnabled = false;
                map.globalWarfogEnabled = false;
            }
            else
            {
                map.globalWarfogEnabled = true;
                map.ReShowWarFog();
            }
        }

        // Game Pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            map.GAMEPAUSED = !map.GAMEPAUSED;
        }

        // Center camera on Player
        if (Input.GetKeyDown(KeyCode.C))
        {
            CenterCameraOnPlayer();
        }

        //TEST instant step
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerUnit.movementCD = 0;
        }
    }
    void LateUpdate()
    {
        float X = Mathf.Clamp(camera.transform.position.x + Input.GetAxis("Horizontal"), cameraXMin, cameraXMax);
        float Z = Mathf.Clamp(camera.transform.position.z + Input.GetAxis("Vertical"), cameraZMin, cameraZMax);
        camera.transform.position = new Vector3(X, camera.transform.position.y, Z);
    }

    public void PlaySetUp(GameObject player)
    {
        playerUnit = player.GetComponent<Unit>();

        CenterCameraOnPlayer();
    }

    private void CenterCameraOnPlayer()
    {
        float X = playerUnit.gameObject.transform.position.x;
        float Z = playerUnit.gameObject.transform.position.z - 4.5f;
        camera.transform.position = new Vector3(X, camera.transform.position.y, Z);
    }
}

