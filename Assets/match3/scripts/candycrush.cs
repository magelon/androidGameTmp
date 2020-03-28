using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Tile need a collider box 2d enable to respone to raycast
//tiles must great than 5 has glitch during only test 3 tiles
//laterly fixed by double the pool size of bank objects
public class Tile
{
    public GameObject tileObj;
    public string type;
    public Tile(GameObject obj, string t)
    {
        tileObj = obj;
        type = t;
    }
}

public class candycrush : MonoBehaviour
{
    public int score=0;
    public Text scoreT;
    //2 tiles for swaping
    GameObject tile1 = null;
    GameObject tile2 = null;
     Camera mainC;

    public GameObject[] tile;
    //tile pool resuable tile objects
    List<GameObject> tileBank = new List<GameObject>();

    static int rows = 9;
    static int cols = 6;
    bool renewBoard = false;
    Tile[,] tiles = new Tile[cols, rows];

    // Use this for initialization
    void Start()
    {
        /*
        mainC = Camera.main;
        mainC.backgroundColor = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                );
        */

        //put number of types of tiles*rows*cols
        //in to tileBank for further use
        int numCopies = (rows * cols);
        
        for (int i = 0; i < numCopies; i++)
        {
            for (int j = 0; j < tile.Length; j++)
            {
                GameObject o = (GameObject)Instantiate(tile[j],
                    new Vector3(-10, -10, 0),
                    tile[j].transform.rotation);
                o.SetActive(false);
                tileBank.Add(o);
            }
        }

        //upset tilebank
        ShuffleList();

        //initialise tile grid
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Vector3 tilePos = new Vector3(c, r, 0);
                for (int n = 0; n < tileBank.Count; n++)
                {
                    GameObject o = tileBank[n];
                    if (!o.activeSelf)
                    {
                        o.transform.position =
                            new Vector3(tilePos.x,
                            tilePos.y, tilePos.z);
                        o.SetActive(true);
                        tiles[c, r] = new Tile(o, o.name);
                        n = tileBank.Count + 1;
                    }
                }
            }
        }

        //update grid from start every 0.5 second 
        InvokeRepeating("CheckGrid", 0.5f,0.5f);

    }
    //initiallize function
    //break the order, randomize the tileBank
    void ShuffleList()
    {
        System.Random rand = new System.Random();
        int r = tileBank.Count;
        while (r > 1)
        {
            r--;
            //return a random number in range of max number
            int n = rand.Next(r + 1);
            GameObject val = tileBank[n];

            tileBank[n] = tileBank[r];
            tileBank[r] = val;
        }
    }

    void Update()
    {
        scoreT.text = score.ToString();
        //Invoke("CheckGrid", 1f);
        
        //GetMouseButton also works on mobile device
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay
                (Input.mousePosition);

            RaycastHit2D hit =
                Physics2D.GetRayIntersection(ray, 1000);
            if (hit)
            {
                tile1 = hit.collider.gameObject;
            }
        }
        else if (Input.GetMouseButtonUp(0) && tile1)
        {
            Ray ray = Camera.main.ScreenPointToRay
                (Input.mousePosition);
            RaycastHit2D hit =
                Physics2D.GetRayIntersection(ray, 1000);
            if (hit)
            {
                tile2 = hit.collider.gameObject;
            }

            if (tile1 && tile2)
            {
                int horzDist = (int)
                    Mathf.Abs(tile1.transform.position.x -
                    tile2.transform.position.x);
                int vertDist = (int)
                    Mathf.Abs(tile1.transform.position.y -
                    tile2.transform.position.y);

                // this statment also works if (horzDist == 1 ^ vertDist == 1)
                if ((horzDist == 1 && vertDist == 0) ||
                (horzDist == 0 && vertDist == 1))
                {
                    //disable tile1 and tile2 sprites
                    tile1.GetComponent<SpriteRenderer>().enabled = false;
                    tile2.GetComponent<SpriteRenderer>().enabled = false;
                    //create two tile with out collider

                    //swap animation for those new tiles with out collider
                    StartCoroutine(SwapAnimation(tile1, tile2, 0.5f));
                    
                    tile1 = null;
                    tile2 = null;
                }
                else
                {
                    //play error audio or any thing
                }

            }


        }
    }

    //extract name form clone
    string ExtractPrefix(GameObject obj)
    {
        string result = obj.name;
        char[] ca = result.ToCharArray();
        for(int i = 0; i < ca.Length; i++)
        {
            if (ca[i] == '(')
            {
                result = result.Substring(0, i);
                //Debug.Log(result);
            }
        }
        return result;
    }


    //swap animation
    IEnumerator SwapAnimation(GameObject tile1,GameObject tile2,float duration)
    {
        //new visual tile1 and tile2
        //need find tile1 position in Tile map
        //name for tile1
        string t1 = ExtractPrefix(tile1);
        Vector3 t1t = tile1.transform.position;
        GameObject fake1 = (GameObject)Instantiate(Resources.Load(t1),t1t,Quaternion.identity);
        string t2 = ExtractPrefix(tile2);
        Vector3 t2t = tile2.transform.position;
        GameObject fake2 = (GameObject)Instantiate(Resources.Load(t2),t2t,Quaternion.identity);
        
        Vector2 initialPos1 = fake1.transform.position;
        Vector2 initialPos2 = fake2.transform.position;

        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / duration;
            fake1.transform.position = Vector2.Lerp(initialPos1, initialPos2,percent);
            fake2.transform.position = Vector2.Lerp(initialPos2, initialPos1, percent);
            yield return null;
        }
        Destroy(fake1);
        Destroy(fake2);
        fake1 = null;
        fake2 = null;

        //swap
        Tile temp = tiles[(int)tile1.transform.position.x, (int)tile1.transform.position.y];

        tiles[(int)tile1.transform.position.x, (int)tile1.transform.position.y] =
            tiles[(int)tile2.transform.position.x, (int)tile2.transform.position.y];

        tiles[(int)tile2.transform.position.x, (int)tile2.transform.position.y] = temp;

        Vector3 tempPos = tile1.transform.position;

        tile1.transform.position = tile2.transform.position;

        tile2.transform.position = tempPos;

        //enable tile1 and tile2 sprites
        tile1.GetComponent<SpriteRenderer>().enabled = true;
        tile2.GetComponent<SpriteRenderer>().enabled = true;
    }

    //dispare animation
    IEnumerator DispareAnimation(GameObject tileObj,int duration,int c,int i,int r)
    {
        GameManager.getInstance().playSfx("crunch");
        //new a same type tile and make it smaller as dispare animation
        Vector3 tt = tileObj.transform.position;
        string type = ExtractPrefix(tileObj);
        GameObject fake = (GameObject)Instantiate(Resources.Load(type),tt,Quaternion.identity);

        Vector3 endScale = new Vector3(0, 0, 0);
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / duration;
            fake.transform.localScale = Vector3.Lerp(fake.transform.localScale, endScale, percent);
            yield return null;
        }
        
        Destroy(fake);
        fake = null;
    }

    IEnumerator RenewDely()
    {
        yield return new WaitForSeconds(1);
        renewBoard = true;
    }


    void CheckGrid()
    {
        int counterCol = 1;
        int counterRow = 1;
        int columMax = 0;
        int columStart = 0;
        int columStartR = 0;
        int rowMax = 0;
        int rowStart = 0;
        int rowStartC = 0;

        //check in colums
        for (int r = 0; r < rows; r++)
        {
            counterCol = 1;
           
            for (int c = 1; c < cols; c++)
            {
                //start check from second one in colums 
                if (tiles[c, r] != null && tiles[c - 1, r] != null)
                //if the tile exist
                {
                    //current one type is equals to previous one
                    if (tiles[c, r].type == tiles[c - 1, r].type)
                    {
                        counterCol++;
                        if (counterCol > columMax)
                        {
                            columMax = counterCol;
                            columStart = c - 1;
                            columStartR = r;
                        }
                           
                        
                    }
                    else
                    {
                        counterCol = 1;//reset counter
                    }
                       
                    /*
                    //if there are found remove 
                    if (counter >= 3)
                    {
                        for(int i = 0; i < counter; i++)
                        {
                            if (tiles[c-i, r] != null)
                            {
                                GameObject tmp = tiles[c - i, r].tileObj;
                               
                                tiles[c - i, r].tileObj.SetActive(false);
                                //play disapare animation
                                //pass in c ,i,r
                                
                                StartCoroutine(DispareAnimation(tmp,1,c,i,r));
                            }
                            tiles[c - i, r] = null;
                            //renewBoard at the end
                          
                        }
                        /*
                        if (tiles[c, r] != null)
                            tiles[c, r].tileObj.SetActive(false);
                        if (tiles[c - 1, r] != null)
                            tiles[c - 1, r].tileObj.SetActive(false);
                        if (tiles[c - 2, r] != null)
                            tiles[c - 2, r].tileObj.SetActive(false);
                        tiles[c, r] = null;
                        tiles[c - 1, r] = null;
                        tiles[c - 2, r] = null;
                        
                        //renewBoard = true;

                    }*/
                }
                /*
                if (r == rows - 1 && c == cols - 1)
                {
                    renewBoard = true;
                }
                */
            }
        }

        //check in rows
      
        for (int c = 0; c < cols; c++)
        {
            counterRow = 1;
            for (int r = 1; r < rows; r++)
            {
                if (tiles[c, r] != null && tiles[c, r - 1] != null)
                //if tiles exist
                {
                    if (tiles[c, r].type == tiles[c, r - 1].type)
                    {
                        counterRow++;
                        if (counterRow > rowMax)
                        {
                            rowMax = counterRow;
                            rowStart = r - 1;
                            rowStartC = c;
                        }
                    }
                    else
                    {
                        counterRow = 1;
                    }
                        
                    /*
                    if (counter >= 3)
                    {
                         for(int i = 0; i < counter; i++)
                        {
                            if (tiles[c, r-i] != null)
                            {
                                GameObject tmp = tiles[c, r-i].tileObj;
                               
                                tiles[c, r-i].tileObj.SetActive(false);
                                //play disapare animation
                                //pass in c ,i,r
                                
                                StartCoroutine(DispareAnimation(tmp,1,c,i,r));
                            }
                            tiles[c, r-i] = null;
                            //renewBoard at the end
                          
                        }
                        //renewBoard = true;
                    }
                    */

                }
            }
        }


        //cleaning board
        //clean colums
        if (columMax >= 3)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 1; c < cols; c++)
                {
                    if (c - 1 == columStart)
                    {
                        for (int i = 0; i < columMax; i++)
                        {
                            if (tiles[c - i, columStartR] != null)
                            {
                                GameObject tmp = tiles[c - i, columStartR].tileObj;

                                tiles[c - i, columStartR].tileObj.SetActive(false);
                                //play disapare animation
                                //pass in c ,i,r

                                StartCoroutine(DispareAnimation(tmp, 1, c, i, columStartR));
                            }
                            tiles[c - i, columStartR] = null;
                        }
                    }
                }
            }
            score = score+2 * columMax;
        }


        //clean rows
        if (rowMax >= 3)
        {
            for (int c = 0; c < cols; c++)
            {
                for (int r = 1; r < rows; r++)
                {
                    if (r - 1 == rowStart)
                    {
                        for (int i = 0; i < rowMax; i++)
                        {
                            if (tiles[rowStartC, r - i] != null)
                            {
                                GameObject tmp = tiles[rowStartC, r - i].tileObj;

                                tiles[rowStartC, r - i].tileObj.SetActive(false);
                                //play disapare animation
                                //pass in c ,i,r

                                StartCoroutine(DispareAnimation(tmp, 1, rowStartC, i, r));
                            }
                            tiles[rowStartC, r - i] = null;
                        }
                    }
                }
            }
            score = score + 2 * rowMax;
        }


        //renew after a full board check
        //StartCoroutine(RenewDely());

        if (rowMax < 3 && columMax < 3)
        {
            renewBoard = true;
        }

        if (renewBoard)
        {
            RenewGrid();
            renewBoard = false;
        }
    }

    void RenewGrid()
    {
        bool anyMoved = false;
        ShuffleList();
        for (int r = 1; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                //row count from bottom top 
                //rows-1 is top row
                if (r == rows - 1 && tiles[c, r] == null)
                //if in the top row and no tile
                {
                    Vector3 tilePos = new Vector3(c, r, 0);
                    //fetch a nonactive tile from tile bank
                    for (int n = 0; n < tileBank.Count; n++)
                    {
                        GameObject o = tileBank[n];
                        if (!o.activeSelf)
                        {
                            o.transform.position = new Vector3(
                                tilePos.x, tilePos.y,
                                tilePos.z
                                );
                            o.SetActive(true);
                            tiles[c, r] = new Tile(o, o.name);
                            n = tileBank.Count + 1;
                        }
                    }
                }
                
                if (tiles[c, r] != null)
                {
                    //drop down if space below is empty
                    if (tiles[c, r - 1] == null)
                    {
                        //tiles[c, r].tileObj.GetComponent<SpriteRenderer>().enabled = false;
                       
                        //start move this tile above null tile
                        tiles[c, r - 1] = tiles[c, r];

                        tiles[c, r - 1].tileObj.GetComponent<SpriteRenderer>().enabled = false;

                        StartCoroutine(DropDownAnimation(c,r));

                        //tiles[c, r - 1].tileObj.transform.position= new Vector3(c, r - 1, 0);

                       


                        //show tiles
                        //tiles[c, r - 1].tileObj.GetComponent<SpriteRenderer>().enabled = true;
                        tiles[c, r] = null;
                        
                        //hide tiles going to move
                        //tiles[c, r].tileObj.GetComponent<SpriteRenderer>().enabled = false;

                       

                        
                        if (tiles[c, r] == null)
                        {
                          //anyMoved = true;
                        }
                        
                        
                    }
                  
                }
            }
        }
        if (anyMoved)
        {
            Invoke("RenewGrid", 1f);
        }
    }

    IEnumerator DropDownAnimation(int c,int r)
    {
        //new fake tile for animation
        //position of real tile
        Vector2 tp = tiles[c, r].tileObj.transform.position;
        //get tiles type from extractprefix
        string tn = ExtractPrefix(tiles[c, r].tileObj);
        //instantiate fake tile by name
        GameObject fakeT =(GameObject) Instantiate(Resources.Load(tn), tp, Quaternion.identity);
        //target position is one unit down 
        Vector2 target = new Vector2(tp.x, tp.y - 1);

        float percent = 0;
        while (percent < 1)
        {                               //drop down speed
            percent += Time.deltaTime / 0.3f;
            fakeT.transform.position = Vector2.Lerp(tp, target, percent);
            yield return null;
        }
        Destroy(fakeT);
        fakeT = null;

        if (tiles[c, r - 1] != null)
        {
            tiles[c, r - 1].tileObj.transform.position
         = new Vector3(c, r - 1, 0);
        }


        /*
                        //start move this tile above null tile
        tiles[c, r - 1] = tiles[c, r];
       
        //show tiles
        
        tiles[c, r].tileObj.GetComponent<SpriteRenderer>().enabled = true;
        tiles[c, r] = null;
        */
        if (tiles[c, r - 1] != null)
        {
            tiles[c, r - 1].tileObj.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}