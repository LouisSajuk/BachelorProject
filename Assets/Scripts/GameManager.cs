using System;
using System.Xml.Serialization;
using TMPro;
using TMPro.EditorUtilities;
using Ubii.Devices;
using Ubii.Services;
using Ubii.TopicData;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerControls _playerControls;
    private PlayerInput _playerInput;
    private UbiiNode _node;
    private TextMeshProUGUI controlUI;

    private bool firstStart = true;
    private Vector3 spawnLocation = new Vector3(0, 0, -34);

    private int _steuerung;
    private int[] _reihenfolge;
    private int _nextSteuerung;

    private string firstName;
    private string lastName;
    private bool reihenfolgeGesetzt = false;
    private String steuerungString;
    private bool alleSteuerungenDurch = false;

    private GameObject[] portals;
    [SerializeField] private UbiiNode ubiiNode;
    Vector3 handyOrientation = new Vector3();
    Vector3 originOrientation = new Vector3();
    Vector3 differenceOrientation = new Vector3();
    private float x_tilt;
    private float y_tilt;
    private bool firstOrigin = true;

    private CinemachineInputAxisController baseAxisController;
    private CinemachineInputAxisController footpedalAxisController;

    //Steuerungen:
    //1 = Claw Grip
    //2 = Fuﬂpedal
    //3 = BackButtons
    //4 = Tilt Controls

    #region GameManager

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is NUll");
            }
            return _instance;
        }
    }


    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);

        Debug.Log("Hello I am your personal GameManager!");

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    #endregion

    void Start()
    {
        portals = new GameObject[4];
        _reihenfolge = new int[4];
        _steuerung = 1;
        _nextSteuerung = 0;

        StartUbiConenction();
    }

    public float giveX_Tilt()
    {
        float value = originOrientation.y - handyOrientation.y;
        if (Math.Abs(value) < 0.03f)
            return 0;

        if (value < 0)
            //return (value - 0.1f) * -100;
            return (float)((Math.Pow(value, 2f)) * 1000);

        if (value > 0)
            //return (value + 0.1f) * -100;
            return (float)((Math.Pow(value, 2f)) * -1000);

        return 0;
    }
    public float giveY_Tilt()
    {
        float value = originOrientation.z - handyOrientation.z;
        if (Math.Abs(value) < 0.03f)
            return 0;

        if (value < 0)
            //return (float)((Math.Pow(value, 3f) - 0.5f) * -80);
            return (float)((Math.Pow(value, 2f)) * 1000);

        if (value > 0)
            //return (float)((Math.Pow(value, 3f) + 0.5f) * -80);
            return (float)((Math.Pow(value, 2f)) * -1000);

        return 0;
    }

    private void Update()
    {
        if (firstOrigin)
        {
            if (handyOrientation != Vector3.zero)
            {
                firstOrigin = false;
                originOrientation = handyOrientation;
            }
        }

        //Debug.Log(originOrientation.ToString());
        //Debug.Log(handyOrientation.ToString());
    }

    public void updateHandyOrigin()
    {
        originOrientation = handyOrientation;
    }

    private async void StartUbiConenction()
    {
        await ubiiNode.WaitForConnection();
        ServiceReply reply = await ubiiNode.CallService(new ServiceRequest
        {
            Topic = UbiiConstants.Instance.DEFAULT_TOPICS.SERVICES.DEVICE_GET_LIST,
            Device = new Ubii.Devices.Device
            {
                Name = "web-interface-smart-device",
                Tags = { new Google.Protobuf.Collections.RepeatedField<string>() { "claw" } },
            }
        });

        /*
        Debug.Log("### DEVICES:");
        foreach (Device device in reply.DeviceList.Elements)
        {
            Debug.Log(device);
        }
        */

        if (reply.DeviceList.Elements.Count != 1)
        {
            Debug.LogError("More or less than one device with tag 'claw', aborting!");
            return;
        }

        Device smartphone = reply.DeviceList.Elements[0];
        reply = await ubiiNode.CallService(new ServiceRequest
        {
            Topic = "/services/component/get_list",
            Component = new Ubii.Devices.Component
            {
                DeviceId = smartphone.Id,
                Tags = { new Google.Protobuf.Collections.RepeatedField<string>() { "orientation" } },
            }
        });
        //Debug.Log("### COMPONENTS:");
        foreach (Ubii.Devices.Component component in reply.ComponentList.Elements)
        {
            if (component.Tags.Contains("orientation"))
            {
                //Debug.Log(component);
                SubscriptionToken subTokenOrientation = await ubiiNode.SubscribeTopic(component.Topic, (TopicDataRecord record) =>
                {
                    //Debug.Log("### TOPIC:");
                    //Debug.Log(record);
                    handyOrientation = new Vector3((float)record.Vector3.X, (float)record.Vector3.Y, (float)record.Vector3.Z).normalized;
                });

                //await ubiiNode.Unsubscribe(subTokenOrientation);
            }
        }
    }

    public int Steuerung
    {
        get { return _steuerung; }
        set { _steuerung = value; }
    }

    public int[] Reihenfolge
    {
        get { return _reihenfolge; }
        set
        {
            _reihenfolge[0] = value[0];
            _reihenfolge[1] = value[1];
            _reihenfolge[2] = value[2];
            _reihenfolge[3] = value[3];
        }
    }

    public void switchSteuerung(int steuerung)
    {
        if (steuerung == 1)
        {
            Steuerung = 1;
            controlUI.text = "Claw Grip";

            if (_playerInput.currentActionMap.ToString() != "Player (UnityEngine.InputSystem.InputActionAsset):Base")
            {
                _playerInput.SwitchCurrentActionMap("Base");
            }

            deactivateFootPedal();
        }
        else if (steuerung == 2)
        {
            Steuerung = 2;
            controlUI.text = "Foot pedal";

            if (_playerInput.currentActionMap.ToString() != "Player (UnityEngine.InputSystem.InputActionAsset):Footpedal")
            {
                _playerInput.SwitchCurrentActionMap("Footpedal");
            }

            activateFootPedal();
        }
        else if (steuerung == 3)
        {
            Steuerung = 3;
            controlUI.text = "Back Button";

            if (_playerInput.currentActionMap.ToString() != "Player (UnityEngine.InputSystem.InputActionAsset):Base")
            {
                _playerInput.SwitchCurrentActionMap("Base");
            }

            deactivateFootPedal();
        }
        else if (steuerung == 4)
        {
            Steuerung = 4;
            controlUI.text = "Tilt Control";
            
            if(_playerInput.currentActionMap.ToString() != "Player (UnityEngine.InputSystem.InputActionAsset):Base")
            {
                _playerInput.SwitchCurrentActionMap("Base");
            }
            
            deactivateFootPedal();
        }
    }

    public bool getAlleSteuerungenDurch()
    {
        return alleSteuerungenDurch;
    }

    public int nextSteuerung()
    {
        Debug.Log("Die vorherige Steuerung : " + _reihenfolge[_nextSteuerung]);

        _nextSteuerung++;

        if (_nextSteuerung >= 4)
        {
            Debug.Log("N‰chste Steuerung wieder 0 weil Error");
            alleSteuerungenDurch = true;
            _nextSteuerung = 0;
        }


        Debug.Log("N‰chste Steuerung wurde erreicht : " + _reihenfolge[_nextSteuerung]);

        return _nextSteuerung;
    }

    public int getSteuerung()
    {
        return _nextSteuerung;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name != "Initializer")
        {
            _playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
            _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
            controlUI = GameObject.Find("ControlTechnique").GetComponent<TextMeshProUGUI>();

            CinemachineInputAxisController[] axisController = GameObject.Find("Third Person Camera").GetComponents<CinemachineInputAxisController>();
            baseAxisController = axisController[0];
            footpedalAxisController = axisController[1];
        }

        if (next.name == "StartingScene")
        {
            if (firstStart)
            {
                firstStart = false;
                GameObject.FindGameObjectWithTag("Player").transform.Translate(spawnLocation);
                PerformanceCatcher.Instance.getReihenfolgeAsStrings();
                PerformanceCatcher.Instance.addEmptyLine();
            }

            changePlayerInput("Base");

            Steuerung = _reihenfolge[_nextSteuerung];

            Debug.Log("Derzeitige Steuerung : " + getSteuerungString());

            portals[0] = GameObject.Find("Portal1").transform.GetChild(1).gameObject;
            portals[1] = GameObject.Find("Portal2").transform.GetChild(1).gameObject;
            portals[2] = GameObject.Find("Portal3").transform.GetChild(1).gameObject;
            portals[3] = GameObject.Find("Portal4").transform.GetChild(1).gameObject;

            Debug.Log("Jetzt Portal Nummer : " + _reihenfolge[_nextSteuerung]);

            portals[0].SetActive(false);
            portals[1].SetActive(false);
            portals[2].SetActive(false);
            portals[3].SetActive(false);

            if (alleSteuerungenDurch)
            {
                portals[0].SetActive(true);
                portals[1].SetActive(true);
                portals[2].SetActive(true);
                portals[3].SetActive(true);
            }
            else
            {
                portals[_reihenfolge[_nextSteuerung] - 1].SetActive(true);
            }

        }


        switchSteuerung(Steuerung);

        Debug.Log("Current Scene : " + next.name);
    }

    public void changePlayerInput(String map)
    {
        _playerInput.SwitchCurrentActionMap(map);
    }


    public void setFirstName(string name)
    {
        firstName = name;
        Debug.Log("First Name : " + firstName);
    }

    public void setLastName(string name)
    {
        lastName = name;
        Debug.Log("Last Name : " + lastName);
    }

    public void setReihenfolge(string reihenfolge)
    {
        if (reihenfolge.Length == 4)
        {
            char[] charfolge = reihenfolge.ToCharArray();

            _reihenfolge = Array.ConvertAll(charfolge, c => (int)Char.GetNumericValue(c));

            reihenfolgeGesetzt = true;
        }
    }

    public bool reihenfolgeIstGesetzt()
    {
        return reihenfolgeGesetzt;
    }

    public void writeReihenfolge()
    {
        for (int i = 0; i < _reihenfolge.Length; i++)
        {
            Debug.Log(_reihenfolge[i]);
        }
    }

    private String getSteuerungString()
    {
        if (Steuerung == 1)
        {
            steuerungString = "Claw Grip";
        }
        else if (Steuerung == 2)
        {
            steuerungString = "Fuﬂpedal";
        }
        else if (Steuerung == 3)
        {
            steuerungString = "BackButtons";
        }
        else if (Steuerung == 4)
        {
            steuerungString = "Tilt Controls";
        }
        return steuerungString;
    }

    private void activateFootPedal()
    {
        footpedalAxisController.enabled = true;
        baseAxisController.enabled = false;
    }

    private void deactivateFootPedal()
    {
        baseAxisController.enabled = true;
        footpedalAxisController.enabled = false;
    }
}
