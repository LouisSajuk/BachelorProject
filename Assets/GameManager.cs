using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private PlayerControls _playerControls;
    private PlayerInput _playerInput;
    private UbiiNode _node;

    private Vector3 spawnLocation;

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

        portals = new GameObject[4];
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    #endregion

    void Start()
    {


        _node = GameObject.FindGameObjectWithTag("UbiiNode").GetComponent<UbiiNode>();
        spawnLocation = new Vector3(0, 0, -34);
        GameObject.FindGameObjectWithTag("Player").transform.Translate(spawnLocation);

        _steuerung = 1;
        _nextSteuerung = 0;

        Debug.Log("Startet n‰chsten Gamemanager");
    }

    public void tester()
    {
        Debug.Log("Testet Anzahl Gamemanager");
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
        _playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        if (next.name == "StartingScene")
        {
            portals[0] = GameObject.Find("Portal1").transform.GetChild(0).gameObject;
            portals[1] = GameObject.Find("Portal2").transform.GetChild(0).gameObject;
            portals[2] = GameObject.Find("Portal3").transform.GetChild(0).gameObject;
            portals[3] = GameObject.Find("Portal4").transform.GetChild(0).gameObject;

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
