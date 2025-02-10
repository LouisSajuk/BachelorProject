using System;
using TMPro;
using TMPro.EditorUtilities;
using Ubii.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerControls _playerControls;
    private PlayerInput _playerInput;
    private UbiiNode _node;

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
    //[SerializeField] private GameObject portal1;
    //[SerializeField] private GameObject portal2;
    //[SerializeField] private GameObject portal3;
    //[SerializeField] private GameObject portal4;

    //public static AudioManager Audio;

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
        //_node = GameObject.FindGameObjectWithTag("UbiiNode").GetComponent<UbiiNode>();

        portals = new GameObject[4];
        _reihenfolge = new int[4];
        _steuerung = 1;
        _nextSteuerung = 0;

        StartTest();
    }

    private async void StartTest()
    {
        if (ubiiNode == null)
        {
            Debug.Log("UbiiClient not found");
            return;
        }

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
        Debug.Log(reply);


        //reply.DeviceList.



        reply = await ubiiNode.CallService(new ServiceRequest
        {
            Topic = "/services/component/get_list",
            ComponentList = new Ubii.Devices.ComponentList
            {
                Elements = {
                    new Google.Protobuf.Collections.RepeatedField< Ubii.Devices.Component>(){
                        new Ubii.Devices.Component {
                            MessageFormat = "ubii.dataStructure.Vector3",
                            Tags = { new Google.Protobuf.Collections.RepeatedField<string>(){"claw"}}
                        }
                    }
                }
            }
        });
        Debug.Log(reply);

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
        }

        if (next.name == "StartingScene")
        {
            if (firstStart)
            {
                firstStart = false;
                GameObject.FindGameObjectWithTag("Player").transform.Translate(spawnLocation);

            }

            changePlayerInput("Base");

            Steuerung = _reihenfolge[_nextSteuerung];
            Debug.Log("Derzeitige Steuerung : " + getSteuerungString());

            portals[0] = GameObject.Find("Portal1").transform.GetChild(0).gameObject;
            portals[1] = GameObject.Find("Portal2").transform.GetChild(0).gameObject;
            portals[2] = GameObject.Find("Portal3").transform.GetChild(0).gameObject;
            portals[3] = GameObject.Find("Portal4").transform.GetChild(0).gameObject;

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

        if (next.name == "Level_1")
        {
            //_playerControls.inputMap = Steuerung;

        }


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
}
