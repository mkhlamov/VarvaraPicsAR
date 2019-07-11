using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour {

    public List<Sprite> m_slides = new List<Sprite>();
    public float m_changeTime = 2.0f;
    public float m_startFade = 0.3f;
    public float m_endFade = 0.7f;
    private int m_currentSlide = -1;
    [SerializeField]
    private Image m_image1;
    [SerializeField]
    private Image m_image2;
    private float m_timer = 0.0f;

    // Use this for initialization
    void Start () {
        SetNewSlide();
    }
	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;

        if (m_timer >= m_changeTime)
        {
            SetNewSlide();
            m_timer = 0.0f;
        } else
        {
            float part = m_timer / m_changeTime;
            if (part > m_startFade && part < m_endFade)
            {
                float timePassed = ((m_timer / m_changeTime) - m_startFade) / (m_endFade - m_startFade);
                SetAlpha(m_image1, 1 - timePassed);
                SetAlpha(m_image2, timePassed );
            }
        }

	}

    void SetAlpha(Image im, float a)
    {
        Color tempColor = im.color;
        tempColor.a = a;
        im.color = tempColor;
    }

    void SetNewSlide()
    {
        m_currentSlide++;
        m_currentSlide %= m_slides.Count;
        SetAlpha(m_image1, 1f);
        SetAlpha(m_image2, 0f);
        m_image1.sprite = m_slides[m_currentSlide % m_slides.Count];
        m_image2.sprite = m_slides[(m_currentSlide + 1) % m_slides.Count];
    }
}
