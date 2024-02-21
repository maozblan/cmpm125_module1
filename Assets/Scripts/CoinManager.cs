using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public GameObject RedCoinPrefab;
    public GameObject BlueCoinPrefab;
    public GameObject RedWallPrefab;
    public GameObject BlueWallPrefab;
    public GameObject WallPrefab;

    // Pooling
    private int MaxCoinCount = 4;
    private List<GameObject> Pool = new();
    private List<GameObject> PoolW = new();
    private List<GameObject> PoolB = new();

    // Location
    private float CoinY = 0.35f;
    private float WallY = 0.0f;
    private List<int> PosNeg = new() { -1, 1 };
    private List<int> CoinXRange = new() { 1, 3, 5, 7, 9, 11 };
    private List<int> CoinZRange = new() { 1, 3, 5 };
    private int WallXMax = 10;
    private int WallZMax = 4;

    // Interaction
    public TextMeshProUGUI CoinHUD;
    public TextMeshProUGUI EndGameScore;
    public int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        // generate coin & wall pools
        GameObject thisSelf = gameObject;
        for (int i = 0; i < MaxCoinCount / 2; ++i)
        {
            float x = RandomInList(CoinXRange) * RandomInList(PosNeg);
            float z = RandomInList(CoinZRange) * RandomInList(PosNeg);
            GameObject NewCoin = Instantiate(RedCoinPrefab, new Vector3(x, CoinY, z), Quaternion.Euler(0f, 0f, 0f));
            NewCoin.GetComponent<Coin>().InstantiateManager(thisSelf, i);
            Pool.Add(NewCoin);

            x = WallXMax - 4 * i;
            z = Random.Range(-WallZMax, WallZMax);
            GameObject NewWall = Instantiate(RedWallPrefab, new Vector3(x, WallY, z), Quaternion.Euler(0f, 0f, 0f));
            PoolW.Add(NewWall);
        }
        for (int i = 0; i < MaxCoinCount / 2; ++i)
        {
            float x = RandomInList(CoinXRange) * RandomInList(PosNeg);
            float z = RandomInList(CoinZRange) * RandomInList(PosNeg);
            GameObject NewCoin = Instantiate(BlueCoinPrefab, new Vector3(x, CoinY, z), Quaternion.Euler(0f, 0f, 0f));
            NewCoin.GetComponent<Coin>().InstantiateManager(thisSelf, MaxCoinCount / 2 + i);
            Pool.Add(NewCoin);

            x = -WallXMax + 4 * i;
            z = Random.Range(-WallZMax, WallZMax);
            GameObject NewWall = Instantiate(BlueWallPrefab, new Vector3(x, WallY, z), Quaternion.Euler(0f, 0f, 0f));
            PoolW.Add(NewWall);
        }

        // randomlly generated other barrier walls
        for (int z = 4; z >= -4; z -= 2)
        {
            float x = Random.Range(-9.0f, 9.0f);
            GameObject NewWall = Instantiate(WallPrefab, new Vector3(x, WallY, z), Quaternion.Euler(0f, 90f, 0f));
            PoolB.Add(NewWall);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RefactorCoin(int id)
    {
        //Debug.Log("refactor coin" + id);
        GameObject Coin = Pool[id];
        GameObject Wall = PoolW[id];

        // regen coin
        float x = RandomInList(CoinXRange) * RandomInList(PosNeg);
        float z = RandomInList(CoinZRange) * RandomInList(PosNeg);
        Coin.transform.position = new Vector3(x, CoinY, z);

        // regen wall
        if (id < MaxCoinCount / 2)
        {
            x = WallXMax - 4 * id;

        }
        else
        {
            x = -WallXMax + 4 * (id - MaxCoinCount / 2);
        }
        z = Random.Range(-WallZMax, WallZMax);
        Wall.transform.position = new Vector3(x, WallY, z);
    }

    public void Reset()
    {
        for (int i = 0; i < MaxCoinCount; ++i)
        {
            RefactorCoin(i);
        }

        List<int> L = new() { -4, -2, 0, 2, 4 };
        for (int i = 0; i < PoolB.Count; ++i)
        {
            float x = Random.Range(-9.0f, 9.0f);
            PoolB[i].transform.position = new Vector3(x, WallY, L[i]);
        }
        Score = 0;
        CoinHUD.text = "SCORE: " + Score;
        EndGameScore.text = "YOUR SCORE: " + Score;
    }

    // random select int in range
    private int RandomInList(List<int> L)
    {
        return L[(int)Random.Range(0, L.Count)];
    }
}
