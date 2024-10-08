using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    [SerializeField] private Button easy;
    [SerializeField] private Button med;
    [SerializeField] private Button hard;
    private float _easySpeed;
    private float _medSpeed;
    private float _hardSpeed;
    
    void Start()
    {
        _easySpeed = 3;
        _medSpeed = 4;
        _hardSpeed = 5;
        easy.onClick.AddListener(() => InitSpeed(_easySpeed));
        med.onClick.AddListener(() => InitSpeed(_medSpeed));
        hard.onClick.AddListener(() => InitSpeed(_hardSpeed));
    }

    private void InitSpeed(float speed)
    {
        PlayerPrefs.SetFloat("speed", speed);
        SceneManager.LoadScene("ScoreMenuScene");
    }
    
}