using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCordAnimation : MonoBehaviour
{
    [SerializeField] float m_rotationSpeed;
    bool clockwise;
    [SerializeField] float noise;
    [SerializeField] float scale;
    [SerializeField] float randomMagnitude;
    [SerializeField] float noiseUpdateTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        clockwise = Random.Range(0,100) > 50 ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > noiseUpdateTime)
        {
            randomMagnitude = Random.Range(-1.0f, 1.0f);
            noise = randomMagnitude + scale;
            timer = 0;
        }
        transform.Rotate(0.0f, 0.0f, (clockwise ? 1.0f : -1.0f) * m_rotationSpeed * noise * Time.deltaTime);
    }
}
