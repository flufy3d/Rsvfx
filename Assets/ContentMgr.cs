using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMgr : MonoBehaviour
{
    public List<GameObject> prefabs;
    // Start is called before the first frame update

    private GameObject current_object;
    private int prefabs_len;
    private int current_index;
    void Start()
    {
        prefabs_len = prefabs.Count;
        if(prefabs_len > 0)
        {
            current_object = Instantiate(prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
            current_index = 0;
        }
        else
        {
            current_object = null;
            current_index = -1;
        }
        
    }

 

    public float keyDelay = 1f;  // 1 second
    private float timePassed = 0f;

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)  && timePassed >= keyDelay)
        {
            // do stuff
            timePassed = 0f;
            if(current_object != null)
            {
                Destroy(current_object);

            }

            current_index += 1;
            if (current_index > prefabs_len - 1) current_index = 0;
            current_object = Instantiate(prefabs[current_index], new Vector3(0, 0, 0), Quaternion.identity);
            

        }
    }
}
