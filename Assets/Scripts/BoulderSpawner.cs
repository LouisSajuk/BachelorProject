using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    [SerializeField] GameObject boulder1;
    [SerializeField] GameObject boulder2;
    [SerializeField] GameObject boulder3;
    [SerializeField] float spawntime;
    private float timeStamp;
    private Vector3 randomPunkt;

    void Start()
    {
        timeStamp = 0;
        randomPunkt.y = 30;
    }

    void Update()
    {
        if (Time.time > timeStamp)
        {
            //min 6 max 25

            Vector2 random2d = Random.insideUnitCircle * 25;

            while (Mathf.Abs(random2d.x) < 6)
            {
                random2d.x += random2d.x;
            }
            while (Mathf.Abs(random2d.y) < 6)
            {
                random2d.y += random2d.y;
            }

            randomPunkt.x = random2d.x;
            randomPunkt.z = random2d.y;

            int auswahl = Random.Range(1, 4);

            if (auswahl == 1)
            {
                GameObject.Instantiate(boulder1, randomPunkt, Quaternion.identity);
            }
            if (auswahl == 2)
            {
                GameObject.Instantiate(boulder2, randomPunkt, Quaternion.identity);
            }
            if (auswahl == 3)
            {
                GameObject.Instantiate(boulder3, randomPunkt, Quaternion.identity);
            }

            timeStamp = Time.time + spawntime;
        }
    }
}
