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

	// Use this for initialization
	void Start () {
        fuelCountText.text = "" + fuelCount;
        //Physics.gravity = new Vector3(0, gravity, 0);
        print(Physics.gravity.y);
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
	
	// Update is called once per frame
	void Update () {
        fuelCountText.text = "" + fuelCount;
    }
}
