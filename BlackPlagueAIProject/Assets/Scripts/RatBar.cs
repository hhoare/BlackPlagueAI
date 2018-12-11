using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBar : MonoBehaviour {

    [SerializeField]
    private RectTransform self;

    private int height;
	
	// Update is called once per frame
	void Update () {
        

		self.sizeDelta = new Vector2(self.sizeDelta.x, 100);
    }
}
