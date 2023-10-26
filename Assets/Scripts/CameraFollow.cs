using UnityEngine;

//this script is attached to the main camera
//it allows the camera to follow the player and their path with slight delay
//for it to work you have to assign a target which is a player GameObject
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; //we're targeting the player duh
    [SerializeField] [Range(0.01f,1f)] //it sets the range of the spped (min ->0.01f; max -> 1f)
    private float cameraSpeed = 1f;
    [SerializeField] private Vector3 offset; //starting distance between player and the camera
    private Vector3 velocity = Vector3.zero; //it's just a Vector3(0,0,0)
    private void LateUpdate() //is called after all of the other updates
    {
        Vector3 newPos = target.position + offset;
        //SmoothDamp function gradually changes a vbalue towards a desired goal over time (at least that's what API says)
        transform.position = Vector3.SmoothDamp(transform.position,newPos,ref velocity, cameraSpeed);

    }
}
