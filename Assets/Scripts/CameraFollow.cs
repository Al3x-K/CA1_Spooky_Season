using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; //we're targeting the player duh
    [SerializeField] [Range(0.01f,1f)]
    private float cameraSpeed = 1f;
    [SerializeField] private Vector3 offset;
    private Vector3 velocity = Vector3.zero; 
    private void LateUpdate() //is called after all of the other updates
    {
        Vector3 newPos = target.position + offset;
        //SmoothDamp function gradually changes a vbalue towards a desired goal over time (at least that's what API says)
        transform.position = Vector3.SmoothDamp(transform.position,newPos,ref velocity, cameraSpeed);

    }
}
