using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoader : MonoBehaviour
{
   [SerializeField] Slider _loader;
   [SerializeField] private GameObject loaderBackground;

   public void LoadSceneASync(string scene)
   {
      loaderBackground.SetActive(true);
      StartCoroutine(LoadLevelASync(scene));
   } 

   IEnumerator LoadLevelASync(string scene)
   {
      AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene);
      while (!loadOperation.isDone)
      {
         float loadingValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
         _loader.value = loadingValue;
         Debug.Log(_loader.value);
         yield return null;
      }
   }
}
