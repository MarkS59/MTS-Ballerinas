using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeAnim : MonoBehaviour
{
    public Image backgroundImage;
    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void Eraser()
    {
        gameObject.GetComponent<Graphic>().DOFade(0, 0);
    }

    public void FadeOut()
    {
        gameObject.GetComponent<Graphic>().DOFade(0.9F, 0.2F);
    }

}
