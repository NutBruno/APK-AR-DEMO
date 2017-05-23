using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFilter : MonoBehaviour {

    public Sprite[] filters;
    public Image imageComponentLandScape;
    public Image TelaPadrao;
    public GameObject[] particulas;
    public int anguloTroca;
    int indexFilter = 0;
    AccelerometerControl control;

    private void Awake()
    {
        control = GetComponent<AccelerometerControl>();
    }
    void Start () {
        indexFilter = 0;
        imageComponentLandScape.sprite = filters[indexFilter];
        foreach (var item in particulas)
        {
            item.SetActive(false);
        }
        particulas[0].SetActive(true);
    }
	
	void Update () {
        if (control.yRotation > 0)
        {
            if (control.yRotation > anguloTroca && control.yRotation <= (anguloTroca*2))
            {
                SelectFilter(0);
                TelaPadrao.enabled = true;
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[0].SetActive(true);
            }
            else if (control.yRotation > (anguloTroca * 2) && control.yRotation <= (anguloTroca * 3))
            {
                SelectFilter(1);
                TelaPadrao.enabled = false;
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[1].SetActive(true);
            }
            else if (control.yRotation > (anguloTroca * 3) && control.yRotation <= (anguloTroca * 4))
            {
                SelectFilter(2);
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[2].SetActive(true);
            }
            else if (control.yRotation > (anguloTroca * 4) && control.yRotation <= (anguloTroca * 5))
            {
                SelectFilter(3);
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[3].SetActive(true);
            }
            else if (control.yRotation > (anguloTroca * 5) && control.yRotation <= (anguloTroca * 6))
            {
                SelectFilter(4);
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
               // particulas[4].SetActive(true);
            }
            else if (control.yRotation > (anguloTroca * 6))
            {
                ResetPosition();
                TelaPadrao.enabled = true;
                SelectFilter(0);
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[0].SetActive(true);
            }
        }else if (control.yRotation < 0)
        {
            if (control.yRotation > -anguloTroca && control.yRotation >= -(anguloTroca * 2))
            {
                SelectFilter(0);
                TelaPadrao.enabled = true;
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[0].SetActive(true);
            }
            else if (control.yRotation < -(anguloTroca * 2) && control.yRotation >= -(anguloTroca * 3))
            {
                SelectFilter(1);
                TelaPadrao.enabled = false;
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[1].SetActive(true);
            }
            else if (control.yRotation < -(anguloTroca * 3) && control.yRotation >= -(anguloTroca * 4))
            {
                SelectFilter(2);
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[2].SetActive(true);
            }
            else if (control.yRotation < -(anguloTroca * 4) && control.yRotation >= -(anguloTroca * 5))
            {
                SelectFilter(3);
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[3].SetActive(true);
            }
            else if (control.yRotation < -(anguloTroca * 5) && control.yRotation >= -(anguloTroca * 6))
            {
                SelectFilter(4);
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
               // particulas[4].SetActive(true);
            }
            else if (control.yRotation < -(anguloTroca * 6))
            {
                ResetPosition();
                SelectFilter(0);
                TelaPadrao.enabled = true;
                foreach (var item in particulas)
                {
                    item.SetActive(false);
                }
                particulas[0].SetActive(true);
            }
        }
    }

    void SelectFilter(int idx)
    {
        if (indexFilter > filters.Length)
        {
            ResetPosition();
            indexFilter = 0;
        }
        else if (indexFilter < 0)
        {
            ResetPosition();
            indexFilter = filters.Length;
        }
        imageComponentLandScape.sprite = filters[idx];
    }

    public  void ResetPosition()
    {
        print("Reset");
        TelaPadrao.enabled = true;
        control.yRotation = 0;
        SelectFilter(0);
        indexFilter = 0;
    }
}
