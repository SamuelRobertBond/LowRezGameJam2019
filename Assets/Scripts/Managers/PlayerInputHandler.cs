using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerController playerController;
    private Dictionary<string, float> pressed;

    // Start is called before the first frame update
    void Start()
    {
        this.pressed = new Dictionary<string, float>();
        this.playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        
        // Cycle Capture Point
        if (pressedBtn("CycleCaptureEntity")) {
            Debug.Log("Cycling Capture Entity");
            this.playerController.cycleCaptureEntityView((int)pressed["CycleCaptureEntity"]);
        }

        // Change Direction of Capture Entity Output
        if (pressedBtn("ChangeDirection")) {
            Debug.Log("Changing Direction");
            this.playerController.nextDirection();
        }

        // Upgrade Units
        if (pressedBtn("BuyUpgrade"))
        {
            Debug.Log("Buying Upgrade");
            this.playerController.upgradeUnits();
        }

        // Buy Units
        if (pressedBtn("BuyLowUnit")) {
            Debug.Log("Buying Low Unit");
            this.playerController.buyUnit(1);
        }

        if (pressedBtn("BuyMediumUnit")) {
            Debug.Log("Buying Medium Unit");
            this.playerController.buyUnit(2);
        }

        if (pressedBtn("BuyHeavyUnit")) {
            Debug.Log("Buying Heavy Unit");
            this.playerController.buyUnit(3);
        }

    }

    private bool pressedBtn(string inputName) {

        float value = Input.GetAxis(inputName);

        // Make sure inputs are initialized to 0 in pressed
        if (!this.pressed.ContainsKey(inputName)) {
            this.pressed[inputName] = 0;
        }

        // If value of input differs from previous input
        if (this.pressed[inputName] != value)
        {
            this.pressed[inputName] = value;
            return value != 0f;
        }

        return false;

    }

}
