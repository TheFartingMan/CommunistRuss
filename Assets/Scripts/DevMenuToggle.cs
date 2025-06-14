using UnityEngine;
using UnityEngine.UI;

public class DevMenuToggle : MonoBehaviour
{
    public GameObject DevMenuGameObject;
    private bool everyOther;
    private float ButtonCooldownTimer;


    void Start()
    {
        ButtonCooldownTimer = 1;
    }



    void Update()
    {
        ButtonCooldownTimer += Time.deltaTime;
        if (Input.GetKey("m") && ButtonCooldownTimer > .25)
        {
            everyOther = !everyOther;

            if (everyOther == true)
            {
                DevMenuGameObject.SetActive(false);
            }

            if (everyOther == false)
            {
                DevMenuGameObject.SetActive(true);
            }

            ButtonCooldownTimer = 0;
        }
    }
    






}
