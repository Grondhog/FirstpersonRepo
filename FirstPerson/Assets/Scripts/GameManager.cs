using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Text fuelCountText;

    [SerializeField]
    private int fuelCount = 100;

   

    private int fuelCountMax = 100;

	// Use this for initialization
	void Start () {
        fuelCountText.text = "" + fuelCount;
	}

    public void decrementFuelCounter()
    {
        if(fuelCount > 0)
        {
            fuelCount--;
        }
    }

    public void incrementFuelCounter()
    {
        if(fuelCount < fuelCountMax)
        {
            fuelCount++;
        }
    }

    public int getFuelCount()
    {
        return fuelCount;
    }
	
	// Update is called once per frame
	void Update () {
        fuelCountText.text = "" + fuelCount;
    }
}
