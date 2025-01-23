using UnityEngine;

public class CylinderCameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2, Player.transform.position.z);
        this.transform.eulerAngles = new Vector3 (-65, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.transform.position;
    }
}
