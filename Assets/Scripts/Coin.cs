using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //private Rigidbody rb;
    private Renderer rd;
    public float RotateSpeed = 2.5f;

    private CoinManager Manager = null;
    private int CoinID = -1;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // link employee to manager
    public void InstantiateManager(GameObject ManagerObj, int id)
    {
        Manager = ManagerObj.GetComponent<CoinManager>();
        CoinID = id;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // hide
            Color color = rd.material.color;
            color.a = 0;
            rd.material.color = color;

            Manager.RefactorCoin(CoinID);

            // fade in
            StartCoroutine(FadeIn());

            // update the score
            Manager.Score++;
            Manager.CoinHUD.text = "SCORE: " + Manager.Score;
            Manager.EndGameScore.text = "YOUR SCORE: " + Manager.Score;
        }
    }

    // coroutine edited from https://docs.unity3d.com/Manual/Coroutines.html
    IEnumerator FadeIn()
    {
        Color c = rd.material.color;
        for (int alpha = 0; alpha <= 10; ++alpha)
        {
            c.a = alpha / 10f;
            rd.material.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}
