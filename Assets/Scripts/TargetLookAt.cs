using UnityEngine;
using UnityEngine.EventSystems;

public class TargetLookAt : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    //[SerializeField] private Rigidbody rb;
    private float timer;
    [SerializeField] private float speed;
    [SerializeField] private float minWidth;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxWidth;
    [SerializeField] private float maxHeight;
    private float width;
    private float height;

    //[SerializeField] private CharacterController controller;
    Vector3 startPos = Vector3.zero;


    private void Start()
    {
        startPos = transform.position;
        timer = 0;
        Player = GameObject.FindWithTag("Player");

        width = Random.Range(minWidth, maxWidth);
        height = Random.Range(minHeight, maxHeight);
    }

    // Update is called once per frame
    void Update()
    {
        timer += (Time.deltaTime * speed);

        //Debug.Log(width);
        //Debug.Log(height);

        float x = startPos.x + Mathf.Cos(timer) * width;
        float y = startPos.y + Mathf.Sin(timer) * height;
        float z = startPos.z;


        transform.position = new Vector3(x, y, z);

        transform.LookAt(Player.transform);
    }
}
