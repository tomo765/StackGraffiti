using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;



public class SceneManagement : MonoBehaviour
{
    [SerializeField] Image[] images;
    [SerializeField] Button[] Buttons;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Button[] LeftRightButton; //右ボタン1 左ボタン0
    [SerializeField] int Pages = 0;
    public int LastPastLevel = 8;//最後レベルのINT
    // Start is called before the first frame update
    void Start()
    {
        LoadPage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            LoadNextPage();
        }
    }

    public void LoadNextPage()
    {
        Pages++;
        LoadPage();

        if (Pages==sprites.Length/images.Length)
        {
            LeftRightButton[1].interactable = false;
        }
        LeftRightButton[0].interactable = true;
    }

    public void LoadPreviousPage()
    {
        Pages--;
        LoadPage();

        if (Pages == 0)
        {
            LeftRightButton[0].interactable = false;
        }
        LeftRightButton[1].interactable = true;
    }




    void LoadPage()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if(sprites.Length>=(images.Length*Pages+i+1)){ 
                Buttons[i].interactable = true;
                images[i].color = Color.white;
                images[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (images.Length*Pages+i+1).ToString();
                images[i].sprite = sprites[images.Length*Pages+i];
            }else{
                Buttons[i].interactable = false;
                images[i].color = new Color(0,0,0,0);
            }

            if(LastPastLevel+1<(images.Length*Pages+i+1)){
                Buttons[i].interactable = false;
            }
        }
    }

    public void LoadScene(int value)
    {
        SceneManager.LoadScene("Stage " + (Pages*images.Length + value));//Stage 14
    }
}
