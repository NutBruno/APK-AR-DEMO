using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCanvas : MonoBehaviour
{
    AccelerometerControl accelerometerControl;
    ChangeFilter filterControl;
    public bool offFilter;
    bool canvasRoutine;
    public int indexTelas;
    public GameObject botoes;
    public Transform[] posicoes;
    public Transform posicaoTela, posicaoForaEsquerda, posicaoForaDireita;
    public float velocidadeTransição;
    public float tempoRoutine;
    

    void Start()
    {
        accelerometerControl = GetComponent<AccelerometerControl>();
        filterControl = GetComponent<ChangeFilter>();
        indexTelas = 0;
        transform.position = posicoes[indexTelas].position;
        canvasRoutine = false;
    }

    void Update()
    {
        if (SwapeManager.Instance.IsSwiping(SwipeDirection.Left))
        {
            print("Swipe Esquerda");
            if (!offFilter)
            {
                offFilter = true;
                indexTelas++;
                if (indexTelas >= posicoes.Length)
                {
                    indexTelas = posicoes.Length - 1;
                }
                StartCoroutine(TelaRoutine());
            }
            if (offFilter && canvasRoutine)
            {
                indexTelas++;
                if (indexTelas >= posicoes.Length)
                {
                    indexTelas = posicoes.Length - 1;
                }
                StartCoroutine(TelaRoutine());
            }
        }
        else if (SwapeManager.Instance.IsSwiping(SwipeDirection.Right))
        {
            print("Swipe Direita");
            if (offFilter && canvasRoutine)
            {
                indexTelas--;
                if (indexTelas <= 0)
                {
                    indexTelas = 0;
                    offFilter = false;
                }
                StartCoroutine(TelaRoutine());
            }
        }
        else if (SwapeManager.Instance.IsSwiping(SwipeDirection.Up) || SwapeManager.Instance.IsSwiping(SwipeDirection.RightUp) || SwapeManager.Instance.IsSwiping(SwipeDirection.LeftUp))
        {
            print("Swipe Cima");
        }
        else if (SwapeManager.Instance.IsSwiping(SwipeDirection.Down))
        {
            print("Swipe Baixo");
        }

        EnableControls(offFilter);
        Animate();
    }

    void EnableControls(bool b)
    {
        if (b)
        {
            if (accelerometerControl.enabled == true)
            {
                accelerometerControl.enabled = false;
                filterControl.ResetPosition();
                filterControl.enabled = false;
                foreach (var item in filterControl.particulas)
                {
                    item.SetActive(false);
                }
            }
        }
        else if (!b)
        {
            accelerometerControl.enabled = true;
            filterControl.enabled = true;
        }
    }

    public void ColocaCanvas(int i)
    {
        print(i);
        if (offFilter)
        {
            if (i == 1)
            {
                indexTelas = 1;
                transform.position = Vector3.Lerp(transform.position, posicoes[indexTelas].position, velocidadeTransição * Time.deltaTime);
            }
            else if (i == 2)
            {
                indexTelas = 2;
                transform.position = Vector3.Lerp(transform.position, posicoes[indexTelas].position, velocidadeTransição * Time.deltaTime);
            }

            else if (i == 3)
            {
                indexTelas = 3;
                transform.position = Vector3.Lerp(transform.position, posicoes[indexTelas].position, velocidadeTransição * Time.deltaTime);
            }
            else if (i == 0)
            {
                if (offFilter)
                {
                    indexTelas = 0;
                    transform.position = Vector3.Lerp(transform.position, posicoes[indexTelas].position, velocidadeTransição * Time.deltaTime);
                    offFilter = false;
                }
            }
            StartCoroutine(TelaRoutine());
        }
        else if (i > 0 && !offFilter)
        {
            transform.position = Vector3.Lerp(transform.position, posicoes[i].position, velocidadeTransição * Time.deltaTime);
            indexTelas = i;
            offFilter = true;
            StartCoroutine(TelaRoutine());
        }
    }

    void Animate()
    {
        transform.position = Vector3.Lerp(transform.position, posicoes[indexTelas].position, velocidadeTransição * Time.deltaTime);
    }
    IEnumerator TelaRoutine()
    {
        yield return new WaitForSeconds(tempoRoutine);
        canvasRoutine = true;
    }
}
