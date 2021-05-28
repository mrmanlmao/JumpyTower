using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public GameObject mainmenu;
    public GameObject hs;
    [SerializeField] TextMeshProUGUI hs1;
    [SerializeField] TextMeshProUGUI hs2;
    [SerializeField] TextMeshProUGUI hs3;
    [SerializeField] TextMeshProUGUI hs4;
    [SerializeField] TextMeshProUGUI hs5;
    [SerializeField] TextMeshProUGUI hs6;
    [SerializeField] TextMeshProUGUI hs7;
    [SerializeField] TextMeshProUGUI hs8;
    [SerializeField] TextMeshProUGUI hs9;
    [SerializeField] TextMeshProUGUI hs10;
    string[] s = new string[10];

    public void HighScores()
    {
        mainmenu.SetActive(false);
        hs.SetActive(true);

        if (File.Exists("highscores.txt"))
        {
            StreamReader sr = new StreamReader("highscores.txt");
            for (int i = 0; i < 10; i++)
            {
                s[i] = sr.ReadLine();
            }
            sr.Close();

          
        }
        else
        {
            StreamWriter sw = new StreamWriter("highscores.txt");
            for (int i = 0; i < 10; i++)
            {
                sw.WriteLine("0");
                
            }
            for (int i = 0; i < 10; i++)
            {
                s[i] = "0";
            }
            sw.Close();
        }
        hs1.text = s[0];
        hs2.text = s[1];
        hs3.text = s[2];
        hs4.text = s[3];
        hs5.text = s[4];
        hs6.text = s[5];
        hs7.text = s[6];
        hs8.text = s[7];
        hs9.text = s[8];
        hs10.text = s[9];
    }

    public void back_hs()
    {
        mainmenu.SetActive(true);
        hs.SetActive(false);
    }

}


