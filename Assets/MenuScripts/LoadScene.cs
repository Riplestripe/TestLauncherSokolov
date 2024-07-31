using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
   
    public GameObject firstGameBtn, firstLoadBtn, firstUnloadBtn, secondGameBtn, secondLoadBtn, secondUnloadBtn;
    [SerializeField] GameObject m_myGameObject;
    [SerializeField] GameObject m_myGameObject2;
    [SerializeField] GameObject errore;
    [SerializeField] GameObject loadingScreen;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
  

    public void LoadData(string AssetName)
    {

        Addressables.LoadAssetAsync<GameObject>(AssetName).Completed +=
         OnLoadDone;
        loadingScreen.SetActive(true);

    }

    private void OnLoadDone(AsyncOperationHandle<GameObject> handle)
    {
        
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            loadingScreen.SetActive(false);

            if (handle.Result.name == "Scene")
            {
                firstGameBtn.GetComponent<Button>().interactable = true;
                firstLoadBtn.GetComponent<Button>().interactable = false;
                firstUnloadBtn.GetComponent<Button>().interactable = true;
                errore.SetActive(false);
                m_myGameObject = handle.Result;

                
            }
            else
            {
                secondGameBtn.GetComponent<Button>().interactable = true;
                secondLoadBtn.GetComponent<Button>().interactable = false;
                secondUnloadBtn.GetComponent<Button>().interactable = true;
                errore.SetActive(false);
                m_myGameObject2 = handle.Result;

            }
        }
        else
        {
            Debug.Log("Problems with internet connection, can't dowaload assets");
            errore.SetActive(true);
            loadingScreen.SetActive(false);

        }
    }

    public void OpenGame()
    {
         Instantiate(m_myGameObject);
        GameObject menu = GameObject.FindGameObjectWithTag("Menu").transform.GetChild(0).gameObject;
        menu.SetActive(false);
    }
    public void OpenGame2()
    {
        Instantiate(m_myGameObject2);
        GameObject menu = GameObject.FindGameObjectWithTag("Menu").transform.GetChild(0).gameObject;
        menu.SetActive(false);
    }

    public void ReleaseData(string scene)
    {
        if (scene == "Scene")
        {
            firstGameBtn.GetComponent<Button>().interactable = false;
            firstLoadBtn.GetComponent<Button>().interactable = true;
            firstUnloadBtn.GetComponent<Button>().interactable = false;
            GameObject firstgame = GameObject.FindGameObjectWithTag("FirstGame");
            Addressables.ReleaseInstance(m_myGameObject);
            Addressables.ClearDependencyCacheAsync(scene);
            Addressables.Release(m_myGameObject);
            Destroy(firstgame);
            m_myGameObject = null;
        }
        else
        {
            secondGameBtn.GetComponent<Button>().interactable = false;
            secondLoadBtn.GetComponent<Button>().interactable = true;
            secondUnloadBtn.GetComponent<Button>().interactable = false;
            GameObject secondgame = GameObject.FindGameObjectWithTag("SecondGame");
            Addressables.ReleaseInstance(m_myGameObject2);
            Addressables.ClearDependencyCacheAsync(scene);
            Destroy(secondgame);
            m_myGameObject2 = null;
        }
       
        
    }

    public void LoadFirstGame()
    {
        LoadData("Scene");
       
    }
    public void LoadSecondGame()
    {
        LoadData("Scene2");

    }

    public void DeleteScene()
    {
        ReleaseData("Scene");
    }
    public void DeleteScene2()
    {
        ReleaseData("Scene2");

    }
}
