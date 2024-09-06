using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AstroidHandler astroidPrefab_1;
    [SerializeField] private AstroidHandler astroidPrefab_2;
    public int astroidCount = 0;
    public int level = 0;

    // Update is called once per frame
    void Update()
    {
        if(astroidCount == 0){
            level++;
            int num = 2 + (level * 2);
            for(int i = 0; i < num; i++){
                createAstroid();
            }
        }
    }
    private void createAstroid(){
        float offset = Random.Range(0f, 1f);
        Vector2 viewSpawn = Vector2.zero;

        int edge = Random.Range(0,4);
        if(edge == 0){
            viewSpawn = new Vector2(offset, 0);
        } else if(edge == 1){
            viewSpawn = new Vector2(offset,1);       
        } else if(edge == 2){
            viewSpawn = new Vector2(0, offset);
        } else if(edge == 3){
            viewSpawn = new Vector2(1, offset);
        }


        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewSpawn);

        int coinFlip = Random.Range(0, 2);
        if(coinFlip == 0){
            Instantiate(astroidPrefab_1, worldSpawnPosition, Quaternion.identity);
        }else if(coinFlip == 1){
            Instantiate(astroidPrefab_2, worldSpawnPosition, Quaternion.identity);
        }
        astroidCount++;
    }

    public void GameOver(){
        StartCoroutine(Restart());
    }

    private IEnumerator Restart(){
        Debug.Log("GAME OVER! YOU DIED!");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return null;
    }
}
