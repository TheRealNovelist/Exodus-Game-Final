using System.Collections;
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
         _loader.value = loadOperation.progress;
         yield return null;
      }
   }
}
