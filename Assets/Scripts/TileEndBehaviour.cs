using System;
using UnityEngine;

public class TileEndBehaviour : MonoBehaviour
{
    [Tooltip("How much time to wait before destroying " +
             "the tile after reaching the end")]
    public float destroyTime = 1.5f;

    [SerializeField] private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.GetComponent<PlayerBehaviour>())
        {
            GameObject.FindObjectOfType<GameController>().SpawnNextTile();

            gm.score++;
            Destroy(transform.parent.gameObject, destroyTime);
        }
    }
}