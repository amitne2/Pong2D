using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    [SerializeField] private Button returnToMenuBtn;
    [SerializeField] private TMP_Text winLossTxt;
    
    void Start()
    {
        winLossTxt.text = PlayerPrefs.GetString("end");
        returnToMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("StartScene"));
    }
    
}
