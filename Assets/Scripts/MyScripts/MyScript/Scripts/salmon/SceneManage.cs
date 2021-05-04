using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManage : MonoBehaviour
{
    private GameObject player;
    private int scene_state;
    private int animation_state;
    private int Max_state;
    private int wheretogo = 0;

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0];
        scene_state = player.GetComponent<test_state>().SceneState;
        animation_state = player.GetComponent<test_state>().AnimationState;
        Max_state = player.GetComponent<test_state>().Max_state;

        if(player.GetComponent<test_state>().timebox)
            player.GetComponent<test_state>().timebox.SetActive(false);
        if(player.GetComponent<test_state>().passwordboard)
            player.GetComponent<test_state>().passwordboard.SetActive(false);
        Debug.Log(scene_state);

        switch (scene.buildIndex)
        {
            case 1:
                //scene 1 room
                //back
                if (scene_state == 2) 
                    player.GetComponent<Transform>().position =new Vector3(0, 3, 3);
                scene_state = 1;
                break;

            case 2:
                //scene 2 schhol
                // anitmation dig
                if (scene_state == 3 && animation_state == 1)
                {
                    animation_state++;
                    SceneManager.LoadScene("Scenes/Animation/Animation-dig");
                }

                //go
                if (scene_state == 1)
                    player.GetComponent<Transform>().position = new Vector3(55,2,2);
                //back
                else if(scene_state == 3)
                    player.GetComponent<Transform>().position = new Vector3(20,2,-38);
                scene_state = 2;
                break;

            case 3: 
                //scene 3 room
                //go
                if (scene_state == 2)
                    player.GetComponent<Transform>().position = new Vector3(0, 0, 0);
                //back
                else if (scene_state == 4)
                    player.GetComponent<Transform>().position = new Vector3(0, 3, 3);
                scene_state = 3;
                break;

            case 4:
                //scene 4 school
                // anitmation confession
                if (scene_state == 3 && animation_state == 0)
                {
                    animation_state++;
                    SceneManager.LoadScene("Scenes/Animation/Animation-confession");
                }
                //go
                if (scene_state == 3)
                    player.GetComponent<Transform>().position = new Vector3(55, 2, 2);
                //back
                else if (scene_state == 5)
                    player.GetComponent<Transform>().position = new Vector3(20, 2, -38);
                scene_state = 4;
                break;

            case 5:
                //scene 5 room
                // anitmation confession
                if (scene_state == 4 && animation_state == 2)
                {
                    animation_state++;
                    SceneManager.LoadScene("Scenes/Animation/Animation-PassAway");
                }
                //go
                if (scene_state == 4)
                    player.GetComponent<Transform>().position = new Vector3(0, 0, 0);
                //back
                else if (scene_state == 6)
                    player.GetComponent<Transform>().position = new Vector3(0, 3, 3);
                scene_state = 5;
                break;

            case 6:
                //scene 6 school
                //go
                if (scene_state == 5)
                    player.GetComponent<Transform>().position = new Vector3(55, 2, 2);
                //back
                else if (scene_state == 7)
                    player.GetComponent<Transform>().position = new Vector3(20, 2, -38);
                scene_state = 6;
                break;

            case 7:
                //scene 7 room
                //go
                if (scene_state == 6)
                    player.GetComponent<Transform>().position = new Vector3(0, 0, 0);
                scene_state = 7;
                break;

            default:
                break;
        }

        player.GetComponent<test_state>().SceneState = scene_state;
        player.GetComponent<test_state>().AnimationState = animation_state;
        player.GetComponent<test_state>().Max_state = Max_state;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void SceneChange(int SceneState)
    {
        wheretogo = SceneState;
        Debug.Log("Load Scene!");
        Invoke("Sc", 2);
    }

    private void Sc()
    {
        SceneManager.LoadScene(wheretogo);
    }
    */
}
