using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject Player;
    private Vector3 oldPlayerLocation;
    [SerializeField] private int distanceToNextTarget;


    void setOldPlayerLocation()
    {
        oldPlayerLocation = Player.transform.position;
        Debug.Log("this is the new checked position = " + oldPlayerLocation.z);
    }

    Transform getPlayerLocation()
    {
        return Player.transform;
    }

    Vector3 newSpawnLocation()
    {
        Vector3 offset = new Vector3(0, 12, 75);

        offset.x += Random.Range(-8,9);
        offset.y += Random.Range(-3,4);


        return getPlayerLocation().position + offset;
        Debug.Log("calculating new spawn position");
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setOldPlayerLocation();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(getPlayerLocation().position.z);
        Debug.Log(oldPlayerLocation.z + distanceToNextTarget);

        if (getPlayerLocation().position.z >= oldPlayerLocation.z + distanceToNextTarget)
        {
            Debug.Log("target should be spawing");

            setOldPlayerLocation();

            Instantiate(Target, newSpawnLocation(), Quaternion.identity);

        }
    }
}
