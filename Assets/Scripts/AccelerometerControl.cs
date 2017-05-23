using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerometerControl : MonoBehaviour {

    bool gyroEnabled;
    Gyroscope gyro;

    GameObject cameraContainer;
    Quaternion rot;

    public float yRotation;
    public float xRotation;

    public Text texto;

    void Start () {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
	}

    void Update()
    {
        //print(yRotation);
        texto.text = string.Format("Y Rotation: {0}", yRotation);
        if (gyroEnabled)
        {
            Input.gyro.enabled = true;
            yRotation += -Input.gyro.rotationRateUnbiased.y;
            xRotation += -Input.gyro.rotationRateUnbiased.x;
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyroEnabled = true;
            Input.gyro.enabled = true;
            cameraContainer.transform.rotation = Quaternion.Euler(0, 0, 0);
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }

        return false;
    }

}
