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

    private const float CHANCE_INTACT = 0.7f;
    private const float CHANCE_BROKEN_LEFT = 0.15f;
    private const float CHANCE_BROKEN_RIGHT = 0.15f;

    // Bestimmung der Wahrscheinlichkeit des Spawns eines kaputten Blocks
    // Wechsel zwischen 0 und den Konstanten, um mehrere Lücken am Stück zu verhindern
    private float chanceIntact = CHANCE_INTACT;
    private float chanceBrokenLeft = CHANCE_BROKEN_LEFT;
    private float chanceBrokenRight = CHANCE_BROKEN_RIGHT;


    private float runningSpeed = 10;

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
        // Start-Array belegen
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
                Vector3 endPos = ((GameObject)bridge[bridge.Count - 1]).transform.position + new Vector3(bridgeLength,0,0);
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
            chanceIntact = CHANCE_INTACT;
            chanceBrokenLeft = CHANCE_BROKEN_LEFT;
            chanceBrokenRight = CHANCE_BROKEN_RIGHT;

        }
        else if(r <= chanceIntact * 100 + chanceBrokenLeft * 100)
        {
            part = Instantiate(bridgeBrokenLeft, pos, Quaternion.Euler(0, 90, 0));
            chanceIntact = 1;
            chanceBrokenLeft = 0;
            chanceBrokenRight = 0;
        }
        else
        {
            part = Instantiate(bridgeBrokenRight, pos, Quaternion.Euler(0, 90, 0));
            chanceIntact = 1;
            chanceBrokenLeft = 0;
            chanceBrokenRight = 0;
        }

        bridge.Add(part);
    }
	#endregion
}
