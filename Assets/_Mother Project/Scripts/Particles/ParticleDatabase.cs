using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDatabase : MonoBehaviour
{   
    public GameObject witchesBolt_ps;
    public GameObject hitWitchesBolt_ps;
    public GameObject PunchStab_ps;
    public GameObject CosmicCatastrophe_ps;
    public GameObject RavenousRoast_ps;
    public GameObject PhantomFury_ps;
    public GameObject AstralAnnihilation_ps;
    public GameObject AstralAnnihilationHit_ps;
    public GameObject counter_go;
    public GameObject devour_go;
    public GameObject arrowPrefab;


    public static ParticleDatabase instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance =  this;
        }
    }


}
