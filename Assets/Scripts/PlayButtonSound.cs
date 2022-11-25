using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    public void PlayConfirmSound(){ FindObjectOfType<AudioManager>().Play("ButtonConfirm"); }
    public void PlayCancelSound() { FindObjectOfType<AudioManager>().Play("ButtonCancel"); }
}
