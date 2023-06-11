using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand : MonoBehaviour
{

    // store hand prefab to be initialised
    public GameObject handPrefab;
    //stores what kind of charecteristics we're looking for with our input Device when we search for it later
    public InputDeviceCharacteristics inputDeviceCharacteristics;

    public bool hideHandOnSelect = false;

    //stores the inputDevice that we're Targiting once we find it in intialisation()
    private InputDevice _targetDevice;
    private Animator _handAnimator;
    private SkinnedMeshRenderer _handMesh;

    // Start is called before the first frame update
    void Start()
    {
        InitializeHand();
    }

    // Update is called once per frame
    void Update()
    {
        //Since our target device might no register at the start of the scene, we continously check until on is found
        if (!_targetDevice.isValid)
        {
            InitializeHand();
        }
        else
        {
            UpdateHand();
        }
    }

    private void InitializeHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //Call Inputdevices to it 
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check of any devices are found here to avoid errors
        if (devices.Count > 0)
        {
            _targetDevice = devices[0];

            GameObject spawnedHand = Instantiate(handPrefab, transform);

            _handAnimator = spawnedHand.GetComponent<Animator>();
            _handMesh = spawnedHand.GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }

    private void UpdateHand()
    {
        //This will get the value for our trigger from the target device and output a float into triggerValue
        if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            _handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            _handAnimator.SetFloat("Grip", 0);
        }
        // This will get the value for our grip from the target device and output a float into gripValue
        if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            _handAnimator.SetFloat("Grip", 0);
        }
    }

    public void hideHandOnselect()
    {
        if (hideHandOnSelect)
        {
            _handMesh.enabled = !_handMesh.enabled;
        }
    }


}
