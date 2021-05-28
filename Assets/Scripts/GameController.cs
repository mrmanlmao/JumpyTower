using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject floor1;
    public GameObject bg;
    public GameObject wall;
    private GameObject[] list = new GameObject[100000];
    public Transform player;
    public Camera cam;
    public int currentFloor = 0;
    public int hightestFloor = 0;
    int z = 1;
    public bool timer = false;
    float timerf = -10;
    public float difficulty = 1.5f;
    public float hard;
    bool cameraMoving = false;
    public float Score;
    public float multiplier = 1.50f;
    public TextMeshProUGUI scoring;
    public Slider time;

    void Start()
    {
        Time.timeScale = 2f;
        BuildLevel(z);
    }

    
    void FixedUpdate()
    {
        if (currentFloor > (100 * z) - 50 && currentFloor < (100 * z)) //creates more floors
        {
            z++;
            BuildLevel(z);
        }
        if (player.position.y > (list[currentFloor].transform.position.y + 1.5)) //enables collider when player passes floor
        {
            list[currentFloor].GetComponent<EdgeCollider2D>().enabled = true;
            currentFloor++;
            

        }
        else if(currentFloor != 0 && player.position.y < list[currentFloor - 1].transform.position.y) 
        {
            currentFloor--;
        }
        for (int i = currentFloor; i < currentFloor + 3; i++) //disables collider when player falls below floor
        {
            list[currentFloor].GetComponent<EdgeCollider2D>().enabled = false;
        }


        if(currentFloor == 2 && !cameraMoving) //camera stars moving
        {
            cameraMoving = true;
            hard = Time.time;
        }
        else if(cameraMoving)
        {
            cam.transform.position += new Vector3(0 , difficulty * Time.deltaTime, 0);
            if(Mathf.Abs(hard - Time.time) > 25)
            {
                hard = Time.time;
                difficulty += 0.5f * (currentFloor / 200);

            }
        }
        
        
        if(cam.transform.position.y - player.position.y > 11.5f)
        {
            Time.timeScale = 0;
            difficulty = 0;
            HighScore();
            SceneManager.LoadScene(0);
            //kraj igre
        }
        else if(player.position.y - cam.transform.position.y > 7f)
        {
            cam.transform.position = new Vector3(0, player.transform.position.y - 7, -10f);
        }

        
        if(Mathf.Abs(timerf - Time.time) > 5)
        {
            multiplier = 1f;
        }
        time.value = Mathf.Abs(timerf - Time.time) / 5;



    }

    public void HighScore()
    {
        if (File.Exists("highscores.txt"))
        {
            StreamReader sr = new StreamReader("highscores.txt");
            string[] s = new string[10];
            for (int i = 0; i < 10; i++)
            {
                s[i] = sr.ReadLine();
            }
            sr.Close();
            bool temp = true;

            for (int i = 0; i < 10 && temp; i++)
            {
                if(Score>Convert.ToInt32(s[i]))
                {
                    temp = false;
                    if (i != 9)
                    {
                        
                        int j = 9;
                        while (j != i)
                        {
                            s[j] = s[j - 1];
                            j--;
                        }
                    }
                    else
                    {
                        s[9] = Convert.ToString(Score);
                    }
                    s[i] = Convert.ToString(Score);
                }
                        
            }

            StreamWriter sw = new StreamWriter("highscores.txt");
            for (int i = 0; i < 10; i++)
            {
                sw.WriteLine(s[i]);
            }
            sw.Close();
        }
        else
        {
            StreamWriter sw = new StreamWriter("highscores.txt");
            sw.WriteLine(Score);
            for (int i = 1; i < 10; i++)
            {
                sw.WriteLine("0");
            }
            sw.Close();
        }
    }

    public void score(int counter)
    {
        if (currentFloor > hightestFloor)
        {
            hightestFloor = currentFloor;
            if (counter == 1)
            {
                Score += 10;
                multiplier = 1f;
                timerf = -10;
            }
            else if (counter > 1)
            {
                Score += counter * 10 * multiplier;
                multiplier += 1f;
                timerf = Time.time;
                time.enabled = true;
            }
            else
            {
                multiplier = 1f;
                timerf = -10;
            }
            scoring.text = "Score: " + Score;
        }
    }


    void BuildLevel(int height)
    {
        height = height - 1;
        Instantiate(bg, new Vector3(0, (height * 5) * 100 + 50, 10), bg.transform.rotation);
        Instantiate(bg, new Vector3(0, (height * 5) * 100 + 150, 10), bg.transform.rotation);
        Instantiate(bg, new Vector3(0, (height * 5) * 100 + 250, 10), bg.transform.rotation);
        Instantiate(bg, new Vector3(0, (height * 5) * 100 + 350, 10), bg.transform.rotation);
        Instantiate(bg, new Vector3(0, (height * 5) * 100 + 450, 10), bg.transform.rotation);

        Instantiate(wall, new Vector3(-18.2f, (height * 5) * 100 - 1 + 50, 0), wall.transform.rotation);
        Instantiate(wall, new Vector3(-18.2f, (height * 5) * 100 - 1 + 150, 0), wall.transform.rotation);
        Instantiate(wall, new Vector3(-18.2f, (height * 5) * 100 - 1 + 250, 0), wall.transform.rotation);
        Instantiate(wall, new Vector3(-18.2f, (height * 5) * 100 - 1 + 350, 0), wall.transform.rotation);
        Instantiate(wall, new Vector3(-18.2f, (height * 5) * 100 - 1 + 450, 0), wall.transform.rotation);
        
        Instantiate(wall, new Vector3(18, (height * 5) * 100 - 1 + 50, 0), wall.transform.rotation);
        Instantiate(wall, new Vector3(18, (height * 5) * 100 - 1 + 150, 0), wall.transform.rotation);
        Instantiate(wall, new Vector3(18, (height * 5) * 100 - 1 + 250, 0), wall.transform.rotation);
        Instantiate(wall, new Vector3(18, (height * 5) * 100 - 1 + 350, 0), wall.transform.rotation);
        Instantiate(wall, new Vector3(18, (height * 5) * 100 - 1 + 450, 0), wall.transform.rotation);

        
        for (int i = ((height + 1) * 100) - 99; i <= 100 * (height + 1); i++)
        {
            float nextpos = UnityEngine.Random.Range(-10f, 10f);
            float nextscale = UnityEngine.Random.Range(1f, 2.5f);
            list[i - 1] = Instantiate(floor1, new Vector3(nextpos, i * 5, 5), floor1.transform.rotation);
            list[i - 1].tag = "Floor";
            list[i - 1].transform.localScale = new Vector3(nextscale, 1, 1);
        }

    }
}
