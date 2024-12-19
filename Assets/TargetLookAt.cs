using UnityEngine;
using UnityEngine.EventSystems;

public class TargetLookAt : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody rb;
    private float timer = 2;

    [SerializeField] private CharacterController controller;


    Vector3 randomDirection() {
        int x = Random.Range(-3, 4);
        int y = Random.Range(-3, 4);
        int z = Random.Range(-3, 4);

        Vector3 dir = new Vector3(x, y, z);
    

        return dir;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;

        if (timer >= 2)
        {
            Debug.Log("in wait");
            controller.Move(randomDirection().normalized * (1 * Time.deltaTime));
            timer = 0;
        }
        


        transform.LookAt(Player.transform);
    }
}
