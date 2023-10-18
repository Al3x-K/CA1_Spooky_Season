using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made with the help of a tutorial from "Root Games" channel on youtube under the name of "Unity 2D: Scrolling Background"
public class LoopingBackground : MonoBehaviour
{
    // Start is called before the first frame update
    public float backgroundSpeed;
    [SerializeField] Renderer bgRenderer;
   

    // Update is called once per frame
    void Update()
    {
        //change the background's image wrap mode to REPEAT before going into "play mode" 
        //changes the offset of the background which allows it to move smoothly 
        bgRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime,0);
    }
}
