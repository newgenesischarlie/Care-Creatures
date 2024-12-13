using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript2 : MonoBehaviour
{
  public void PlayGame ()
      {
        SceneManager.LoadSceneAsync(1);
    }
}
