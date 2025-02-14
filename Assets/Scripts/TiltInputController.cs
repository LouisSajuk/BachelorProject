using UnityEngine;
using Unity.Cinemachine;
using System;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TiltInputController : InputAxisControllerBase<TiltInputController.TiltController>
{
    void Update()
    {
        if (Application.isPlaying)
            UpdateControllers();
    }

    [Serializable]
    public class TiltController : IInputAxisReader
    {
        public String axis;
        public Connector connector;

        public float GetValue(Object context, IInputAxisOwner.AxisDescriptor.Hints hint)
        {
            if (axis == "Horizontal")
                return connector.x_tilt;

            if (axis == "Vertical")
                return connector.y_tilt;

            return 0;
        }
    }
}
