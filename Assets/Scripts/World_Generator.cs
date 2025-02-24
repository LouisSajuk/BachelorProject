using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class World_Generator : MonoBehaviour
{
    [SerializeField] private Transform Knuepfpunkt;
    private Transform alterKnuepfpunkt;
    [SerializeField] private GameObject CrosswayNachLinks;
    [SerializeField] private GameObject CrosswayNachRechts;
    [SerializeField] private GameObject Corridor;
    [SerializeField] private GameObject Deadend;
    [SerializeField] private GameObject Finish;
    private GameObject temp;
    private int ersteRichtung;
    private int zweiteRichtung;

    private List<GameObject> generatedObjects = new List<GameObject>();

    private int[] currentMaze = new int[12];
    private int counter;

    private int[] wrongMaze1 = new int[] { 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1 };
    private int[] wrongMaze2 = new int[] { 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
    private int[] wrongMaze3 = new int[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1 };
    private int[] wrongMaze4 = new int[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 };
    private int[] wrongMaze5 = new int[] { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1 };
    private int[] wrongMaze6 = new int[] { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0 };


    private int[] wrongMaze7 = new int[] { 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 };
    private int[] wrongMaze8 = new int[] { 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 };
    private int[] wrongMaze9 = new int[] { 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0 };
    private int[] wrongMaze10 = new int[] { 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1 };
    private int[] wrongMaze11 = new int[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0 };
    private int[] wrongMaze12 = new int[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1 };

    private int Richtung;


    void Start()
    {
        //QualitySettings.shadowDistance = 200f;
        alterKnuepfpunkt = Knuepfpunkt;

        generateMaze();

        while (compareMaze1() || compareMaze2() || compareMaze3() || compareMaze4() || compareMaze5() || compareMaze6() || compareMaze7() || compareMaze8() || compareMaze9() || compareMaze10() || compareMaze11() || compareMaze12())
        {
            Debug.Log("Maze wurde falsch generiert!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            for (int i = 0; i < generatedObjects.Count; i++)
            {
                Destroy(generatedObjects[i]);
            }
            generatedObjects.Clear();
            generateMaze();
        }
    }

    void generateMaze()
    {
        Knuepfpunkt = alterKnuepfpunkt;

        //1 = norden, 2 = osten, 3 = süden, 4 = westen
        Richtung = 1;

        //links = 0, rechts = 1
        ersteRichtung = -1;
        zweiteRichtung = -1;

        counter = 0;

        for (int i = 0; i < 24; i++)
        {
            //spawn Crossway
            if (i % 2 == 0)
            {
                if (ersteRichtung == 0 && zweiteRichtung == 0)
                {
                    spawnCrossWayRight();

                    RichtungNachRechts();

                    ersteRichtung = zweiteRichtung;
                    zweiteRichtung = 1;

                    currentMaze[counter] = zweiteRichtung;

                    //Debug.Log(currentMaze[counter]);

                    counter++;
                }
                else if (ersteRichtung == 1 && zweiteRichtung == 1)
                {
                    spawnCrossWayLeft();

                    RichtungNachLinks();

                    ersteRichtung = zweiteRichtung;
                    zweiteRichtung = 0;

                    currentMaze[counter] = zweiteRichtung;

                    //Debug.Log(currentMaze[counter]);

                    counter++;
                }
                else
                {
                    int decider = Random.Range(0, 2);

                    if (decider == 0)
                    {
                        spawnCrossWayLeft();

                        RichtungNachLinks();

                        ersteRichtung = zweiteRichtung;
                        zweiteRichtung = 0;

                        currentMaze[counter] = zweiteRichtung;

                        //Debug.Log(currentMaze[counter]);

                        counter++;
                    }
                    else if (decider == 1)
                    {
                        spawnCrossWayRight();

                        RichtungNachRechts();

                        ersteRichtung = zweiteRichtung;
                        zweiteRichtung = 1;

                        currentMaze[counter] = zweiteRichtung;

                        //Debug.Log(currentMaze[counter]);

                        counter++;
                    }
                    else
                    {
                        Debug.Log("Hilfe bei Generierung");
                        break;
                    }
                }

            }
            //spawn Corridor
            else
            {
                spawnCorridor();

            }
        }

        spawnFinish();

        //Debug.Log("jetzt das current Maze:");
        for (int i = 0; i < 12; i++)
        {
            //Debug.Log(currentMaze[i]);
        }
    }

    bool compareMaze1()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze1[i])
            {
                //Debug.Log("false - ist nicht Maze1!");
                return false;
            }
        }


        Debug.Log("true - Maze1 generiert!");
        return true;
    }
    bool compareMaze2()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze2[i])
            {
                //Debug.Log("false - ist nicht Maze2!");
                return false;
            }
        }

        Debug.Log("true - Maze2 generiert!");
        return true;
    }
    bool compareMaze3()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze3[i])
            {
                //Debug.Log("false - ist nicht Maze3!");
                return false;
            }
        }

        Debug.Log("true - Maze3 generiert!");
        return true;
    }
    bool compareMaze4()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze4[i])
            {
                //Debug.Log("false - ist nicht Maze4!");
                return false;
            }
        }

        Debug.Log("true - Maze4 generiert!");
        return true;
    }
    bool compareMaze5()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze5[i])
            {
                //Debug.Log("false - ist nicht Maze5!");
                return false;
            }
        }

        Debug.Log("true - Maze5 generiert!");
        return true;
    }
    bool compareMaze6()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze6[i])
            {
                //Debug.Log("false - ist nicht Maze6!");
                return false;
            }
        }

        Debug.Log("true - Maze6 generiert!");
        return true;
    }
    bool compareMaze7()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze7[i])
            {
                //Debug.Log("false - ist nicht Maze7!");
                return false;
            }
        }

        Debug.Log("true - Maze7 generiert!");
        return true;
    }
    bool compareMaze8()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze8[i])
            {
                //Debug.Log("false - ist nicht Maze8!");
                return false;
            }
        }

        Debug.Log("true - Maze8 generiert!");
        return true;
    }
    bool compareMaze9()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze9[i])
            {
                //Debug.Log("false - ist nicht Maze9!");
                return false;
            }
        }

        Debug.Log("true - Maze9 generiert!");
        return true;
    }
    bool compareMaze10()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze10[i])
            {
                //Debug.Log("false - ist nicht Maze10!");
                return false;
            }
        }

        Debug.Log("true - Maze10 generiert!");
        return true;
    }
    bool compareMaze11()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze11[i])
            {
                //Debug.Log("false - ist nicht Maze11!");
                return false;
            }
        }

        Debug.Log("true - Maze11 generiert!");
        return true;
    }
    bool compareMaze12()
    {
        for (int i = 0; i < 12; i++)
        {
            if (currentMaze[i] != wrongMaze12[i])
            {
                //Debug.Log("false - ist nicht Maze12!");
                return false;
            }
        }

        Debug.Log("true - Maze12 generiert!");
        return true;
    }

    void RichtungNachRechts()
    {
        Richtung++;
        if (Richtung == 5)
        {
            Richtung = 1;
        }
    }

    void RichtungNachLinks()
    {
        Richtung--;
        if (Richtung == 0)
        {
            Richtung = 4;
        }
    }

    private void spawnCrossWayRight()
    {
        //Debug.Log(Richtung + " für CrossWayRight");
        if (Richtung == 1)
        {
            temp = Instantiate(CrosswayNachRechts, Knuepfpunkt.position, Quaternion.identity);
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(Instantiate(Deadend, temp.transform.GetChild(1).position, Quaternion.Euler(0, 270, 0)));
            generatedObjects.Add(temp);
        }
        else if (Richtung == 2)
        {
            temp = Instantiate(CrosswayNachRechts, Knuepfpunkt.position, Quaternion.Euler(0, 90, 0));
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(Instantiate(Deadend, temp.transform.GetChild(1).position, Quaternion.identity));
            generatedObjects.Add(temp);
        }
        else if (Richtung == 3)
        {
            temp = Instantiate(CrosswayNachRechts, Knuepfpunkt.position, Quaternion.Euler(0, 180, 0));
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(Instantiate(Deadend, temp.transform.GetChild(1).position, Quaternion.Euler(0, 90, 0)));
            generatedObjects.Add(temp);
        }
        else if (Richtung == 4)
        {
            temp = Instantiate(CrosswayNachRechts, Knuepfpunkt.position, Quaternion.Euler(0, 270, 0));
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(Instantiate(Deadend, temp.transform.GetChild(1).position, Quaternion.Euler(0, 180, 0)));
            generatedObjects.Add(temp);
        }
        else
        {
            Debug.Log("Hilfe bei Generierung von CrosswayRechts");
        }
    }

    private void spawnCrossWayLeft()
    {
        //Debug.Log(Richtung + " für CrossWayLeft");
        if (Richtung == 1)
        {
            temp = Instantiate(CrosswayNachLinks, Knuepfpunkt.position, Quaternion.identity);
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(Instantiate(Deadend, temp.transform.GetChild(1).position, Quaternion.Euler(0, -270, 0)));
            generatedObjects.Add(temp);
        }
        else if (Richtung == 2)
        {
            temp = Instantiate(CrosswayNachLinks, Knuepfpunkt.position, Quaternion.Euler(0, 90, 0));
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(Instantiate(Deadend, temp.transform.GetChild(1).position, Quaternion.Euler(0, -180, 0)));
            generatedObjects.Add(temp);
        }
        else if (Richtung == 3)
        {
            temp = Instantiate(CrosswayNachLinks, Knuepfpunkt.position, Quaternion.Euler(0, 180, 0));
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(Instantiate(Deadend, temp.transform.GetChild(1).position, Quaternion.Euler(0, -90, 0)));
            generatedObjects.Add(temp);
        }
        else if (Richtung == 4)
        {
            temp = Instantiate(CrosswayNachLinks, Knuepfpunkt.position, Quaternion.Euler(0, 270, 0));
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(Instantiate(Deadend, temp.transform.GetChild(1).position, Quaternion.identity));
            generatedObjects.Add(temp);
        }
        else
        {
            Debug.Log("Hilfe bei Generierung von CrosswayLinks");
        }
    }

    private void spawnCorridor()
    {
        int first = Random.Range(1, 3);
        int second = Random.Range(3, 5);



        //Debug.Log(Richtung + " für Corridor");
        if (Richtung == 1)
        {
            temp = Instantiate(Corridor, Knuepfpunkt.position, Quaternion.identity);
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(temp);

            temp.transform.GetChild(first).gameObject.SetActive(false);
            temp.transform.GetChild(second).gameObject.SetActive(false);
        }
        else if (Richtung == 2)
        {
            temp = Instantiate(Corridor, Knuepfpunkt.position, Quaternion.Euler(0, 90, 0));
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(temp);

            temp.transform.GetChild(first).gameObject.SetActive(false);
            temp.transform.GetChild(second).gameObject.SetActive(false);
        }
        else if (Richtung == 3)
        {
            temp = Instantiate(Corridor, Knuepfpunkt.position, Quaternion.Euler(0, 180, 0));
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(temp);

            temp.transform.GetChild(first).gameObject.SetActive(false);
            temp.transform.GetChild(second).gameObject.SetActive(false);
        }
        else if (Richtung == 4)
        {
            temp = Instantiate(Corridor, Knuepfpunkt.position, Quaternion.Euler(0, 270, 0));
            Knuepfpunkt = temp.transform.GetChild(0);
            generatedObjects.Add(temp);

            temp.transform.GetChild(first).gameObject.SetActive(false);
            temp.transform.GetChild(second).gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Hilfe bei Generierung von Corridor");
        }
    }

    void spawnFinish()
    {
        //Debug.Log(Richtung + " für Finish");
        if (Richtung == 1)
        {
            temp = Instantiate(Finish, Knuepfpunkt.position, Quaternion.identity);
            generatedObjects.Add(temp);
        }
        else if (Richtung == 2)
        {
            temp = Instantiate(Finish, Knuepfpunkt.position, Quaternion.Euler(0, 90, 0));
            generatedObjects.Add(temp);
        }
        else if (Richtung == 3)
        {
            temp = Instantiate(Finish, Knuepfpunkt.position, Quaternion.Euler(0, 180, 0));
            generatedObjects.Add(temp);
        }
        else if (Richtung == 4)
        {
            temp = Instantiate(Finish, Knuepfpunkt.position, Quaternion.Euler(0, 270, 0));
            generatedObjects.Add(temp);
        }
        else
        {
            Debug.Log("Hilfe bei Generierung von Finish");
        }
    }
}
