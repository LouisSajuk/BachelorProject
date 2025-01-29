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

    private GameObject[] portals;
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
        _steuerung = 1;
        _nextSteuerung = 0;
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
        Debug.Log("Die vorherige Steuerung : " + _nextSteuerung);

        _nextSteuerung++;

        if (_nextSteuerung >= 4)
        {
            Debug.Log("N‰chste Steuerung wieder 0 weil Error");
            _nextSteuerung = 0;
        }


        Debug.Log("N‰chste Steuerung wurde erreicht : " + _nextSteuerung);

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


            portals[0] = GameObject.Find("Portal1").transform.GetChild(0).gameObject;
            portals[1] = GameObject.Find("Portal2").transform.GetChild(0).gameObject;
            portals[2] = GameObject.Find("Portal3").transform.GetChild(0).gameObject;
            portals[3] = GameObject.Find("Portal4").transform.GetChild(0).gameObject;

            //nextSteuerung ist hier noch falsch, wird verbessert
            Debug.Log("Jetzt Portal Nummer : " + _nextSteuerung);

            portals[0].SetActive(false);
            portals[1].SetActive(false);
            portals[2].SetActive(false);
            portals[3].SetActive(false);

            portals[_nextSteuerung].SetActive(true);
        }





        Debug.Log("Current Scene: " + next.name);
    }
}
