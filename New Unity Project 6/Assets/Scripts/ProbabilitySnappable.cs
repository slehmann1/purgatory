using UnityEngine;
using System.Collections;

public class ProbabilitySnappable : Snappable {


    public definiteSpawn[] definitelySpawns;
    public bool destroyChildren;
    public bool makeChild;
    [System.Serializable]
    public class definiteSpawn
    {
        public possibility[] possibles;
    }
    [System.Serializable]
    public class possibility
    {
        public GameObject obj;
        public float probability;
    }
    void Start()
    {
        for (int i = 0; i < definitelySpawns.Length; i++)
        {
            System.Array.Sort(definitelySpawns[i].possibles,
                   delegate(possibility x, possibility y)
                   {
                       return x.probability.CompareTo(y.probability);
                   });//sorts by probability from lowest to highest
            float amount = 0;
            for (int x = 0; x < definitelySpawns[i].possibles.Length; x++)
            {
                amount += definitelySpawns[i].possibles[x].probability;
            } if (amount != 100)
            {
                Debug.LogError("Probabilities do not add to one hundred!");
            }
        }
    }

    public void destroy()
    {
        showParticles();
        CancelInvoke();
        GameObject g;
        if (destroyChildren)
        {
            foreach (Transform child in transform)
            {
                Destroy((child.gameObject));
            }
        }
        for (int i = 0; i < definitelySpawns.Length; i++)
        {
            //decides the possible 
            int ind = 0;
            float f = UnityEngine.Random.Range(0, 100);
            float amount = 0;
            for (int index = 0; index < definitelySpawns[i].possibles.Length; index++)
            {
                amount += definitelySpawns[i].possibles[index].probability;
                if (amount >= f)
                {
                    ind = index;
                    break;
                }
            }
            g = (GameObject)GameObject.Instantiate(definitelySpawns[i].possibles[ind].obj);
            if (makeChild)
            {
                g.transform.parent = this.transform;
            }
            else
            {
                g.transform.parent = transform.parent;
            }
            g.transform.localPosition = g.transform.position;
            g.transform.localScale = g.transform.lossyScale;
            g.layer = gameObject.layer;
        }
        Component[] comps = GetComponents<Component>();
        for (int i = 0; i < comps.Length; i++)
        {
            if (comps[i] != this && !comps[i].ToString().Contains("Transform"))
            {
                Destroy(comps[i]);
            }
        }
        if (transform.parent != null && transform.parent.GetComponent<ChildHingesConfigurator>())
        {
            transform.parent.GetComponent<ChildHingesConfigurator>().act();
        }
        if (transform.parent != null && transform.parent.GetComponent<ChildDistanceJointSetup>())
        {
            transform.parent.GetComponent<ChildDistanceJointSetup>().act();
        }
        if (makeChild)
        {
            Destroy(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
