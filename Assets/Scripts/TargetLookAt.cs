using UnityEngine;
using UnityEngine.EventSystems;

public class TargetLookAt : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    //[SerializeField] private Rigidbody rb;
    private float timer;
    private float speed;
    private float width;
    private float height;

    //[SerializeField] private CharacterController controller;
    Vector3 startPos = Vector3.zero;


    private void Start()
    {
        startPos = transform.position;
        timer = 0;
        speed = 2;
        width = 2;
        height = 2;
        Player = GameObject.FindWithTag("Player");
    }

    /*
    Vector3 randomDirection() {
        int x = Random.Range(-3, 4);
        int y = Random.Range(-3, 4);
        int z = Random.Range(-3, 4);

        Vector3 dir = new Vector3(x, y, z);
    

        return dir;
    }
    */

    // Update is called once per frame
    void Update()
    {
        
        timer += (Time.deltaTime * speed);

        /*
        if (timer >= 2)
        {
            Debug.Log("in wait");
            controller.Move(randomDirection().normalized * (1 * Time.deltaTime));
            timer = 0;
        }
        */

        float x = startPos.x + Mathf.Cos(timer) * width;
        float y = startPos.y + Mathf.Sin(timer) * height;
        float z = startPos.z;


        transform.position = new Vector3(x, y, z);

        transform.LookAt(Player.transform);
    }
}
