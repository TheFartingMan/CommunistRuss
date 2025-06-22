using System;
using System.Text;
using UnityEngine;

public class Aimer_Movement : MonoBehaviour
{
    public Transform AimerTransform;
    public double Value_for_rotation;

    public double Aimerx;
    public double Aimery;
    public float accelerationSpeed;
    public float decelerationSpeed;
    public float rotationSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationSpeed = rotationSpeed - accelerationSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationSpeed = rotationSpeed + accelerationSpeed;
        }
        //if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        //{
        if (rotationSpeed > 0.001f || rotationSpeed < -0.001f)
        {
            rotationSpeed = decelerationSpeed * rotationSpeed;
        }
        else
        {
            rotationSpeed = 0f;
        }


        //}
        Value_for_rotation += rotationSpeed * Time.deltaTime;
        Aimerx = Math.Cos(Value_for_rotation * Math.PI / 180);
        Aimery = Math.Sin(Value_for_rotation * Math.PI / 180);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (float)Value_for_rotation - 90));
        transform.position = new Vector3((float)Aimerx, (float)Aimery, transform.position.z);
    }


    private void UpgradeHandling()
    {
        accelerationSpeed *= 1.25f;
        decelerationSpeed *= .99f;
    }
}
