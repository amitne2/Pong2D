using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startBtn;

    void Start()
    {
        startBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelsMenuScene");
        });
    }
}