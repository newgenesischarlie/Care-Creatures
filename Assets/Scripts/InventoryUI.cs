using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class toggle : MonoBehaviour

{

   public void whenButtonClicked()
{
gameObject.SetActive(!gameObject.activeInHierarchy);
}

}
