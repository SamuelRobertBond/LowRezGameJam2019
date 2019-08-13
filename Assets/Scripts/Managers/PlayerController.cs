using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerManager
{

    public int selectedCaptureEntityIndex; // Used to cycle capture entities


    // Snaps the camera to the next capture entity that the player owns
    public void cycleCaptureEntityView(int direction) {

        this.selectedCaptureEntityIndex += direction;

        if (capturedEntities.Count == 0) {
            return;
        }

        // Set the Index
        if (this.selectedCaptureEntityIndex >= capturedEntities.Count)
        {
            this.selectedCaptureEntityIndex = 0;
        }
        else if (this.selectedCaptureEntityIndex < 0)
        {
            this.selectedCaptureEntityIndex = capturedEntities.Count - 1;
        }

        // Set Current Capture Entity
        selectedCaptureEntity = capturedEntities[selectedCaptureEntityIndex];
        Debug.Log("Selected Entity: " + selectedCaptureEntityIndex);

        //TODO: tell the camera manager to go to the selected capture entity
    }

    public void nextDirection() {
        capturedEntities[selectedCaptureEntityIndex].changeDirection();
    }

}
