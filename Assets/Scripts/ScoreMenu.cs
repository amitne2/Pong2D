using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] private Button threePoints;
    [SerializeField] private Button fivePoints;
    [SerializeField] private Button sevenPoints;
    private int _shortGamePoints;
    private int _normalGamePoints;
    private int _longGamePoints;
    
    void Start()
    {
        _shortGamePoints = 3;
        _normalGamePoints = 5;
        _longGamePoints = 7;
        threePoints.onClick.AddListener(() => InitPoints(_shortGamePoints));
        fivePoints.onClick.AddListener(() => InitPoints(_normalGamePoints));
        sevenPoints.onClick.AddListener(() => InitPoints(_longGamePoints));
    }

    private void InitPoints(int points)
    {
        PlayerPrefs.SetInt("target", points);
        SceneManager.LoadScene("GameScene");
    }
    
}
