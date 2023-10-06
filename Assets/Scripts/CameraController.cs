using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start() { player = GameObject.FindWithTag("Player").GetComponent<Transform>(); }


    // Update is called once per frame
    void Update()
    {
        Vector3 updater = new Vector3(player.position.x, player.position.y, -10);
        transform.position = updater;
    }
}