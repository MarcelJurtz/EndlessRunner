/*
* Copyright (C) Marcel Jurtz
* twitter.com/MarcelJurtz
*/
using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

    #region Variables
    public GameObject bridgeIntact;
    public GameObject bridgeBrokenLeft;
    public GameObject bridgeBrokenRight;
    public GameObject cam;
    public GameObject player;

    private float chanceIntact = 0.7f;
    private float chanceBrokenLeft = 0.15f;
    private float chanceBrokenRight = 0.15f;

    private float runningSpeed = 1;

    private ArrayList bridge;
    private int bridgeParts = 20;
    private float bridgeLength = 3f;
    private Vector3 currentBridgePartPosition;

    private System.Random rand = new System.Random();
	#endregion

	#region Methods
	void Start () {
        bridge = new ArrayList();
        currentBridgePartPosition = player.transform.position;
        currentBridgePartPosition.y -= 3;
        // populateArray
        for(int i = 0; i < bridgeParts;i++)
        {
            loadNewBridgePart(currentBridgePartPosition);
            //partPosition.z += bridgeIntact.GetComponent<Renderer>().bounds.size.z;
            // TODO
            currentBridgePartPosition.x += bridgeLength;
        }
	}
    
	void Update () {

        player.transform.Translate(Vector3.right * runningSpeed * Time.deltaTime);

        for (int i = bridge.Count -1; i >= 0; i--)
        {
            GameObject currentBridgePart = (GameObject)bridge[i];

            // -10 als Puffer für Kamera
            if (currentBridgePart.transform.position.x < (player.transform.position.x - 10))
            {
                Destroy(currentBridgePart);
                bridge.RemoveAt(i);

                // Neuen Part hinzufügen
                Vector3 endPos = ((GameObject)bridge[bridge.Count - 1]).transform.position + new Vector3(2,0,0);
                loadNewBridgePart(endPos);
            }
        }
	}

    void loadNewBridgePart(Vector3 pos)
    {
        GameObject part;
        int r = rand.Next(0, 101);

        if(r <= chanceIntact * 100)
        {
            part = Instantiate(bridgeIntact, pos, Quaternion.Euler(0, 90, 0));
            Debug.Log("Intact");
        } else if(r <= chanceIntact * 100 + chanceBrokenLeft * 100)
        {
            part = Instantiate(bridgeBrokenLeft, pos, Quaternion.Euler(0, 90, 0));
            Debug.Log("Broken Left");
        } else
        {
            part = Instantiate(bridgeBrokenRight, pos, Quaternion.Euler(0, 90, 0));
            Debug.Log("Broken Right");
        }

        //Debug.Log("Instatiating at: " + pos);
        //part.transform.rotation = Quaternion.Euler(0, 90, 0);
        //part.transform.position = pos;
        //Instantiate(part.transform);

        bridge.Add(part);
    }
	#endregion
}
