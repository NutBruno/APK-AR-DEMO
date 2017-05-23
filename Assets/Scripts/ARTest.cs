using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARTest : MonoBehaviour {
    // Gyro
    Gyroscope gyro;
    GameObject cameraContainer;
    Quaternion rotation;
    public float yRotation;
    public float xRotation;

    //Cam
    WebCamTexture cam;
    public RawImage background;
    public AspectRatioFitter fit;

    bool arReady = false;

	void Start () {
        //Verifica se pode usar os componentes
        //Gyroscope
        if (!SystemInfo.supportsGyroscope)
        {
            print("O dispositivo não tem um gyroscope");
            return;
        }

        //Back Camera
        for (int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            if (WebCamTexture.devices[i].isFrontFacing)
            {
                cam = new WebCamTexture(WebCamTexture.devices[i].name, Screen.width, Screen.height);
                break;
            }
        }

        //Se não houver camera
        if (cam == null)
        {
            print("Camera não encontrada!");
            return;
        }

        //Se tudo estiver disponivel
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyro = Input.gyro;
        gyro.enabled = true;
        cameraContainer.transform.rotation = Quaternion.Euler(90f, 0, 0);
        rotation = new Quaternion(0, 0, 1, 0);

        cam.Play();
        background.texture = cam;

        arReady = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (arReady)
        {
            //Atualiza camera
            float ratio = (float)cam.width / (float)cam.height;
            fit.aspectRatio = ratio;

            float scaleY = cam.videoVerticallyMirrored ? -1f : 1f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -cam.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

            //Atualiza Gyro
            yRotation += -Input.gyro.rotationRateUnbiased.y;
            xRotation += -Input.gyro.rotationRateUnbiased.x;

            transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
        }
	}
}
