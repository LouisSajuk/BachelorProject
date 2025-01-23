using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] private GameObject Player;


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
    }
}
