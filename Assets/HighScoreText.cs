using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text scoreText = GetComponent<Text>();
        scoreText.text = LevelManager.score.ToString();
        LevelManager.score = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
