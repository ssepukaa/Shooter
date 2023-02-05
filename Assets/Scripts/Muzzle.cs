using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float BulletSpeed;
    public AudioSource ShotSound;

    
    void Update()
    {
        if (SimpleInput.GetAxis("HorizontalRight") > 0.9f || SimpleInput.GetAxis("HorizontalRight") < -0.9f ||
            SimpleInput.GetAxis("VerticalRight") > 0.9f || SimpleInput.GetAxis("VerticalRight") < -0.9f)
        {
            GameObject newBullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = 
                new Vector3(SimpleInput.GetAxis("HorizontalRight"), 0, SimpleInput.GetAxis("VerticalRight")) * BulletSpeed;

            ShotSound.pitch = Random.Range(0.8f, 1.2f);
            ShotSound.Play();
        }
    }
}
