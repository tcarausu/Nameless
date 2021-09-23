using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using UnityEngine;

public class SimplePortalScript : MonoBehaviour
{
    public Transform destination;
    public GameObject player;

    public List<SpriteRenderer> runes;
    public float lerpSpeed;

    private Color curColor;
    private Color targetColor;

    public AudioSource source;
    public AudioClip soundClip;


    void OnTriggerEnter2D(Collider2D portal)
    {
        if (portal.CompareTag("Player"))
        {
            targetColor = new Color(1, 1, 1, 1);
            StartCoroutine(RegainStaminaOverTime());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        targetColor = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

        foreach (var r in runes)
        {
            r.color = curColor;
        }
    }

    private IEnumerator RegainStaminaOverTime()
    {
        // curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);
        //
        // foreach (var r in runes)
        // {
        //     r.color = curColor;
        // }


        source.PlayDelayed(1f);
        source.PlayOneShot(soundClip);
        yield return new WaitForSeconds(lerpSpeed + 0.1f);

        Vector3 pos = Vector3.zero;
        pos = destination.transform.position;



        Vector3 endPosition = new Vector3(pos.x, pos.y, 0);
        player.transform.position = endPosition;
    }
}