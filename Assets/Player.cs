using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public float speed;
    [SyncVar (hook="RpcOnChangeColor")] private Color color;

    // Start is called before the first frame update
    void Start()
    {
        if(this.isLocalPlayer) {
            color = new Color(Random.value, Random.value, Random.value, Random.value); 
            CmdCanviaColor(color);   
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isLocalPlayer) {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f) * speed * Time.deltaTime);
        }
    }

    [ClientRpc] void RpcOnChangeColor(Color c) {
        color = c;
        SyncColor();
    }

    [Command(channel=0)] void CmdCanviaColor( Color esteColor) {
        color = esteColor;
        SyncColor();
    }

    void SyncColor(){ 
        GetComponent<Renderer>().material.color = color;
    }
}
