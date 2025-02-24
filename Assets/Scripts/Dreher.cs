using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dreher : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(0f, Time.deltaTime * -10, 0f);
    }
}
