using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int _health;
    [SerializeField]
    Type _enemyType;
    [SerializeField]
    GameObject _markerPrefab;
    [SerializeField]
    GameObject _deathPrefab;

    GameObject _marker;

    Vector3 _nextMove;
    bool _ilyaunpontici = true;

    Vector3 _previousPosition;

    enum Type { Infantry, Plane, Tank };
    Dictionary<Type, Vector3> _moveDictionnary = new Dictionary<Type, Vector3>()
    {
        {Type.Infantry, Vector3.forward},
        {Type.Tank, Vector3.forward},
        {Type.Plane, Vector3.forward*2},
    };




    public void OnTriggerEnter(Collider other) //What happens when an enemy collides with a thingy that does something 
    {
        if (other.transform.CompareTag("Damage"))
        {
            _health -= other.GetComponent<DamageSpike>().Damages;
            if(_health <= 0)
            {
                GameObject deathMark = Instantiate(_deathPrefab);
                deathMark.transform.position = this.transform.position;
                Destroy(this.gameObject);
                if (_marker != null)
                {
                    Destroy(_marker);
                }

            }
        }

        if (other.transform.CompareTag("Info"))
        {
            if (_marker == null)
            {
                _marker = Instantiate(_markerPrefab, null);
            }
            _marker.transform.position = this.transform.position;
        }
    }

    public void StartTurn()
    {
        if(_previousPosition == null)
        {
            _previousPosition = this.transform.position;
        };

        _nextMove = _moveDictionnary[_enemyType];
        
        if(_enemyType == Type.Infantry)
        {
            CheckForObstacle();
        }
        if (_enemyType == Type.Tank)
        {
            CheckForRoads();
        }

        _previousPosition = this.transform.position;
        Move(_nextMove);
    }
    public void CheckForRoads()
    {
        RaycastHit hit;
        List<Vector3> directions = new List<Vector3>();

        // Check for roads in 3 directions and adds them to list so we can select them later
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1)) //Check forward
        {
            Debug.Log("Road ahead!");
            if (hit.collider.CompareTag("Road"))
            {
                if (_previousPosition.x > this.transform.position.x || _previousPosition.x < this.transform.position.x)
                {
                    _nextMove = Vector3.forward;
                    return;
                }
                directions.Add(Vector3.forward);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.left, out hit, 1)) //Check left
        {
            if (hit.collider.CompareTag("Road"))
            {
                if(this.transform.position + Vector3.left != _previousPosition)
                {
                    directions.Add(Vector3.left);
                }
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right, out hit, 1))  //Check right
        {
            if (hit.collider.CompareTag("Road"))
            {
                if (this.transform.position + Vector3.right != _previousPosition)
                {
                    directions.Add(Vector3.right);
                }
            }
        }

        if (directions.Count > 0)
        {
            _nextMove = directions[Random.Range(0, directions.Count)];
        }
        else
        {
            _nextMove = Vector3.forward;
        }

    }
    public Vector3 CheckNearestBridge() //Check all bridges in the scene, take the nearest one, and determine if it's left or right from the player
    {
        GameObject[] bridges = GameObject.FindGameObjectsWithTag("Bridge");

        if (bridges.Length != 0)
        {
            GameObject nearestBridge = bridges[0];

            for (int i = 0; i < bridges.Length; i++)
            {
                if (bridges[i].transform.position.z < this.transform.position.z)
                {
                    continue;
                }

                if (bridges[i].transform.position.z - this.transform.position.z <= nearestBridge.transform.position.z - this.transform.position.z)
                {
                    nearestBridge = bridges[i];
                }
            }

            if (nearestBridge.transform.position.x >= this.transform.position.x)
            {
                return Vector3.right;
            }
            else
            {
                return Vector3.left;
            }
        }
        return Random.value > 0.5f ? Vector3.right : Vector3.left; //If there's  bridge just throw a random position
    }

    public void CheckForObstacle()
    {
        RaycastHit hit;

        // Check for obstacle FORWARD
        if (Physics.Raycast(transform.position, _nextMove, out hit, 1))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Obstacle");
                // If an obstacle is detected, choose left or right based on nearest bridge
                _nextMove = CheckNearestBridge();


                //Chefk LEFT or RIGHT
                if (Physics.Raycast(transform.position, _nextMove, out hit, 1))
                {
                    if (hit.collider.CompareTag("Obstacle"))
                    {
                        Debug.Log("Obstacle");
                        //If an obstacle is deteced, go the other way.
                        _nextMove *= -1;

                        //Check other option
                        if (Physics.Raycast(transform.position, _nextMove, out hit, 1))
                        {
                            if (hit.collider.CompareTag("Obstacle"))
                            {
                                Debug.Log("Obstacle");
                                // If an obstacle is detected, choose left or right randomly
                                _nextMove = Vector3.zero;
                                return;
                            }
                        }

                    }
                }

                return;
            }
        }
    }

    //public void Check
    public void Move(Vector3 movement)
    {
        this.transform.position += movement;
    }
}

/*                                                                                                                                    
                                                                           -::::-----                                                                                   
                                                                         -::-----#%---:-                                                                                
                                                                       ::::-------=------                                                                               
     ::.---                                                           -::---------@@@--+-------- --:----                                                                
     :.--------                                                       -:----------+#=-+------+@*:----*#----                                                             
    :..-----------                                                  ---:------------------:---*:------@%-#--                                                            
    :..---------------                                          ---+#--::---------------+-:---:-------==-+--=                                                           
     ::....--------------                                    --:----*--:::---------------::---:-----------=--------                                                     
        :::::-:--------------                               -:------*#---:::-----::::::::------:-----------+**++++++==                                                  
             :::...-------------                          =*=-------#%-*----::+******=----------::--:::---****#%#*****+-                                                
                 ::....-------------                    ----:--:-------------**#*##***-----------------------+*#*###***--                                               
                    -::...:------------               -------:---------------*###**=--------------------------+***###*+---                                              
                        ::....:------------         ----------:::--::::------*##**+-----------------------------***#*--@#-                                              
                             :.....-----------    -------------==-------------+**+-------------------------------------@%-                                              
                                -::...:---------@@%--------%%=-----#--------------=-------@*------------------------+%%+-*%@                                            
                                    =+:...:-----@@++=-------++-****-=+------------*****=-===%@%+------=-=++*+=#===+==-==+**+#@@@@@                                      
                                @@@@@@@@@+....:@@@@@###*@@**+++*+**-+%====----=**-----**-++*##+=--=++@+-+++*=##==*=+==-++**==+*@@@@@@@@                                 
                           @@@@@@@@@@@@@@@@%---=@@@@@@@@@@@@%++***#=#%+==+=-:-==**-:-=#=-++*+=##-*##@*-+=+**#####==+*==++***+==+*@@@@@@@@@@                             
                        @@@@@@@@@@@@@@@@@@%*++++*++#@@@@@@@@@+@@**#-+%%*****+=-:-*=-----=+**###*#%@@==+=++*####*++++=-++**##*+==++@@@@@@@@@@@@@                         
                     @@@@@@@@@@@@@@@@@@%++++*###*+++*+#@@@@@@#+@##@@%+%*---------=+*#=-=++*##%%@@@#++=-=+*##*++++*==--++*####*+=+++@@@@@@@@@@@@@@                       
                   @@@@@@@@@@@@@@@@@@#++++*#*###*++++**#@@@@@@++@**%%@%#####%@@@@@@@%%%%##**++++++++++++++++++++**#--++**####*++****#@@@@@@@@@@@@@@@                    
                 @@@@@@@@@@@@@@@@@@@+++**===+++=*+++--**#*---------------=@@@*++++=+++***#######*==+++*++=**--+++=--++***###****###*#**@@@@@@@@@@@@@@@                  
               @@@@@@@@@@@@@@@@@@@%+++*=+**++++=*+++++*=---=++**#**##*##%@@*++++*****+****++==----=+++*#+**=-==---=+***#*###*#*###*****#@@@@@@@@@@@@@@@                 
              @@@@@@@@@@@@@@@@@@@@+++***====+=*%=++++*=-=+***+++*@*****##*-+=-------=====------=+**-=##+++=-----=+***#####****#*#*+*#%@@@@@@@@@@@@@@@@@@                
             @@@@@@@@@@@@@@@@@@@@%++%**%#%@@##===*++++*=*#=**##*+***#%#**+==----------------=-------------==++****######***+++++-++#@@@@@@@@@@@@@@@@@@@@@               
             @@@@@@@@@@@@@@@@@@@@%++@#*%##@@@##@@@++****+##--*##*#####*=-::---=---=------=++****####***+=++=-++*#####**++###%%--+++@@@@@@@@@@@@@@@@@@@@@@@              
             @@@@@@@@@@@@@@@@@@@@@++#@**%%#%@@%#%%%+++****##=-+%#####**+++++***###+---=+****####****+*+++=-=++****++++++*#*@#--=++@@@@@@@@@@@@@@@@@@@@@@@%              
              @@@@@@@@@@@@@@@@@@@@@++%@#*#%##%@@%###+++****####%#*****#*****++++++--=+***+++++++++++++++++++++++++++#%@@@@%=--=++@@@@@@@@@@@@@@@@@@@@@@@@+              
              #@@@@@@@@@@@@@@@@@@@@@*++@@#**=----------==--===--=--*****+++++===++*****+=+*#######-----=++*##%@@@@@@@@@@%---=++@@@@@@@@@@@@@@@@@@@@@@@@@++              
              +*@@@@@@@@@@@@@@@@@@@@@@*+++----=+++******++%%*+*%#***+-+++===++*********####*+=------=+*%@@@@@@@@@@@@%*----=++%@@@@@@@@@@@@@@@@@@@@@@@@@++               
               ++@@@@@@@@@@@@@@@@@@@@@@@@#+=++***++++*#%@@@#*++++++=-==-------------------------==+*****++++===--------=+*%@@@@@@@@@@@@@@@@@@@@@@@@@@*++*               
                *++%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%%%%%%%*=-----------------------====+++***###%%%%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@*+++                 
                  *++*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=-----=++%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%++++                   
                    *+++*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#----++#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%+++++                     
                       +++++#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=-+++@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@*++++++                       
                          ++++++*%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#***+++++                          
                              *+++++++#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@*++++++++                               
                                  *++++++****%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#*++***++++*                                   
                                        +++***++++++**#%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%%#*++++++++**+++*                                        
                                               +*++++++++++++++++******##%%@@@@@@@@@@@@@@@@@%%##*****++++++++++++++++++*+                                               
                                                          **++++++++++++++++++++++++++++++++++++++++++++++++*                                                           
                                                                                ##****##                                                                                
*/                                                                                                      
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
                                                                                                                                                                        
