using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance = null;
    public static ScoreManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    protected Text scoreUI;

    private int score;
    public int Score
    {
        get { return score; }
        protected set { score = value >= 0 ? value : 0; }
    }

    void Awake()
    {
        EnsureSingleton();
    }

    private void EnsureSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        scoreUI.text = Score.ToString();
    }

    public virtual void UpdateScoreBy(int update)
    {
        Score += update;
    }

    public virtual void TurnEnds()
    {
        UpdateScoreBy(-2);
    }
}