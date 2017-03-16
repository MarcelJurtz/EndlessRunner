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
    private int bridgeParts = 10;
    private float bridgeLength = 3f;

    private System.Random rand = new System.Random();
	#endregion

	#region Methods
	void Start () {
        bridge = new ArrayList();
        Vector3 partPosition = player.transform.position;
        partPosition.y -= 3;
        // populateArray
        for(int i = 0; i < bridgeParts;i++)
        {
            loadNewBridgePart(partPosition);
            //partPosition.z += bridgeIntact.GetComponent<Renderer>().bounds.size.z;
            // TODO
            partPosition.x += bridgeLength;
        }
	}
    /*
	void Update () {
		for(int i = bridge.Count -1; i >= 0; i++)
        {
            GameObject currentBridgePart = (GameObject)bridge[i];
            if(currentBridgePart.transform.position.z < cam.transform.position.z)
            {
                // Element löschen wenn außer Sichtweite
                bridge.RemoveAt(i);
                bridge.Add(bridgeIntact);
            }
        }
	}
    */
    void loadNewBridgePart(Vector3 pos)
    {
        GameObject part;
        int r = rand.Next(0, 101);

        if(r <= chanceIntact * 100)
        {
            part = bridgeIntact;
            Debug.Log("Intact");
        } else if(r <= chanceIntact * 100 + chanceBrokenLeft * 100)
        {
            part = bridgeBrokenLeft;
            Debug.Log("Broken Left");
        } else
        {
            part = bridgeBrokenRight;
            Debug.Log("Broken Right");
        }

        Debug.Log("Instatiating at: " + pos);
        part.transform.rotation = Quaternion.Euler(0, 90, 0);
        part.transform.position = pos;
        Instantiate(part.transform);
    }
	#endregion
}
