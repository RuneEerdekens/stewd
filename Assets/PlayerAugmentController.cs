using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerEvents))]
public class PlayerAugmentController : MonoBehaviour
{
    private PlayerEvents eventScript;
    public GameObject menuScreen;
    private bool isUiActive;

    private void Awake()
    {
        eventScript = gameObject.GetComponent<PlayerEvents>();

    }

    private void Update()
    {
        isUiActive = Input.GetKey(KeyCode.E);
        menuScreen.SetActive(isUiActive);
        GetComponent<PlayerAttackController>().enabled = !isUiActive;
        Time.timeScale = isUiActive ? 0 : 1;
    }

    public void add()
    {
        eventScript.dashEvent.AddListener(test);
        print("test");
    }

    void test(Vector3 _)
    {
        print("hallo");
    }
}
