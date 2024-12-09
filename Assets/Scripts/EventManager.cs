using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    GameObject torch;
    GameObject fireplace;
    GameObject crystalball;
    GameObject scroll;
    GameObject exit_hidden;
    GameObject exit;

    private bool fireplace_activated = false;
    private bool crystalball_activated = false;
    private bool magic_activated = false;

    // Start is called before the first frame update
    void Start()
    {
        torch = GameObject.FindGameObjectWithTag("Torch");
        fireplace = GameObject.FindGameObjectWithTag("Fireplace");
        crystalball = GameObject.FindGameObjectWithTag("CrystalBall");
        scroll = GameObject.FindGameObjectWithTag("Scroll");
        exit_hidden = GameObject.FindGameObjectWithTag("ExitHidden");
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.CompareTag("Exit"))
            {
                exit = obj;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireplace_activated && Vector3.Distance(crystalball.transform.position, fireplace.transform.position) < 2f)
        {
            ActivateCrystalBall();
        }
        if (crystalball_activated && scroll != null && Vector3.Distance(scroll.transform.position, crystalball.transform.position) < 1f)
        {
            ActivateMagic();
        }
        if (magic_activated && exit_hidden != null)
        {
            ExposeExit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (torch != null && gameObject == fireplace && collision.gameObject == torch)
        {
            ActivateFireplace();
        }
    }

    private void ActivateFireplace() {
        Transform light = fireplace.transform.Find("Point Light");
        if (light != null)
        {
            light.GetComponent<Light>().intensity = 5;
        }
        fireplace_activated = true;
        torch.SetActive(false);
    }

    private void ActivateCrystalBall()
    {
        crystalball.GetComponent<Light>().intensity = 5;
        crystalball_activated = true;
    }

    private void ActivateMagic()
    {
        crystalball.GetComponent<Light>().color = new Color(1f, 0f, 0f); // Red
        crystalball.GetComponent<Light>().range = 10;
        magic_activated = true;
        scroll.SetActive(false);
    }

    private void ExposeExit()
    {
        exit_hidden.SetActive(false);
        exit.SetActive(true);
    }
}
