using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LoS edited from https://pastebin.com/vtq6vFEe

public class Enemy : MonoBehaviour
{

    private int Range = 8;
    private int Direction = 1;
    [SerializeField] private float Speed = 2f;

    // managers
    public GameObject UIManagerObj;
    private UIManager Manager;

    [SerializeField] private float viewDistance = 25.0f;
    private GameObject player;
    int layerMask;

    private float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        // layer mask for LoS
        player = GameObject.FindGameObjectWithTag("Player");
        layerMask = 1 << 10;
        layerMask = ~layerMask;

        Manager = UIManagerObj.GetComponent<UIManager>();

        // save for later
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.GamePlaying)
        {
            // line of sight
            Vector3 direction = player.transform.position - transform.position;

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, viewDistance, layerMask, QueryTriggerInteraction.Ignore))
            {

                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(transform.position, direction, Color.white);
                    // chase
                    transform.Translate(Speed * 1.7f * Time.deltaTime * new Vector3(direction.x, 0f, direction.z).normalized);
                }
                else
                {
                    Debug.DrawRay(transform.position, direction, Color.red);
                    // idle
                    Bounce();
                }
            }
        }
    }

    void Bounce()
    {
        transform.Translate(Speed * Time.deltaTime * new Vector3(0f, 0f, Range*Direction).normalized);
        if (transform.position.z >= Range)
        {
            Direction = -1;
        }
        if (transform.position.z <= -Range)
        {
            Direction = 1;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Manager.GameOver();
        }
    }

    public void Reset()
    {
        this.transform.position = new Vector3(x, y, z);
    }
}
