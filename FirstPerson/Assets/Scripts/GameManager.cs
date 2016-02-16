using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Text fuelCountText;

    [SerializeField]
    private int fuelCount = 100;

  	private int fuelCountMax = 100;

  	[SerializeField]
  	private float gravity = -80f;

	
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

    public void  fillFuel()
    {
        fuelCount = fuelCountMax;
    }

    public void UpgradeFuelLimit(int amount)
    {
    	fuelCountMax += amount;
    }
	
	// Update is called once per frame
	void Update () {
        fuelCountText.text = "" + fuelCount;
    }
}
