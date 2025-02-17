using UnityEngine;

public class Connector : MonoBehaviour
{
    public float x_tilt = 0f;
    public float y_tilt = 0f;

    void Update()
    {
        x_tilt = Mathf.Clamp(GameManager.Instance.giveX_Tilt(), -180, 180);
        y_tilt = Mathf.Clamp(GameManager.Instance.giveY_Tilt(), -180, 180);

        //Debug.Log("X-Value : " + x_tilt + ", Y-Value : " + y_tilt);

        /*
        x_tilt = GameManager.Instance.giveX_Tilt() * -200;
        y_tilt = GameManager.Instance.giveY_Tilt() * -200;
        Debug.Log("Current x Value : " + x_tilt + " , and y Value : " + y_tilt);
        */
    }
}
