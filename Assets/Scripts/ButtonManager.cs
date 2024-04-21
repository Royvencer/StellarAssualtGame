using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    private List<InstantiateShips> instantiateShips = new List<InstantiateShips>();
    public Button[] buttons;
    private Button SelectedButton;

    // Start is called before the first frame update
    public void SelectButton(Button button) {
        if(SelectedButton == button) {
            DeselectButton();
            return;
        }
        if (SelectedButton != null)
        {
            TextMeshProUGUI selectedButtonText = SelectedButton.GetComponentInChildren<TextMeshProUGUI>();
            selectedButtonText.fontStyle = FontStyles.Normal;
        }
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.fontStyle = FontStyles.Bold;


        SelectedButton = button;

        foreach(InstantiateShips instantiateShip in instantiateShips)
        {
            instantiateShip.enabled = false;
        }
        button.GetComponentInChildren<InstantiateShips>().enabled = true;

        //Debug.Log(button.name);
    }

    public void DeselectButton() {
        if (SelectedButton != null)
        {
            TextMeshProUGUI selectedButtonText = SelectedButton.GetComponentInChildren<TextMeshProUGUI>();
            selectedButtonText.fontStyle = FontStyles.Normal;
            foreach (InstantiateShips instantiateShip in instantiateShips)
            {
                instantiateShip.enabled = false;
            }
            SelectedButton = null;
        }
    }

    void Start() {
        foreach (Button button in buttons) {
            instantiateShips.Add(button.GetComponentInChildren<InstantiateShips>());
            instantiateShips[instantiateShips.Count - 1].enabled = false;
            button.onClick.AddListener(() => {
                //Debug.Log("BUTTON WAS CLICKED! " + button.name);
                SelectButton(button);
            });
            
        }
        InstantiateShips.OnShipInstantiated += DeselectButton;
    }

    // Update is called once per frame
    void OnDestroy() {
        InstantiateShips.OnShipInstantiated -= DeselectButton;
    }
    void Update() {
 
    }
}
