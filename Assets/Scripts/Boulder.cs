using Unity.VisualScripting;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    Vector3 position;
    bool isFLying;
    [SerializeField] GameObject Warnungsfeld;
    GameObject feld;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFLying = true;
        position = transform.position;
        position.y = 0;

        feld = GameObject.Instantiate(Warnungsfeld, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= 0.1)
        {
            gameObject.transform.position = position;
            isFLying = false;
            GameObject.Destroy(feld);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ouch by Boulder!!!");
            collision.gameObject.GetComponent<PlayerControls>().ouch();

            PerformanceCatcher.Instance.incOuchRock();

            GameObject.Destroy(feld);
            GameObject.Destroy(gameObject);
        }
    }

    public bool IsFlying() {  return isFLying; }
}
