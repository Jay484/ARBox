using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestBottomMenu : MonoBehaviour
{
    public BottomMenu menu;
    string[] urls;
    string url = "https://images.pexels.com/photos/19613282/pexels-photo-19613282/free-photo-of-man-surfing-on-waves.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2";
    int size = 15;
    // Start is called before the first frame update
    bool load = true;
    void Start()
    {
        urls = new string[15];
        for(int i = 0; i < size; i++)
        {
            urls[i] = url;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (load)
        {
            load = false;
            //menu.TestLoad(url);
            //menu.LoadUrls(urls, size);
        }
    }
}
