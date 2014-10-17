using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;

[CustomEditor(typeof(ConnectViaDuplication))]
[CanEditMultipleObjects]
public class ConnectViaDuplicationEditor : Editor {
    private float oldAngle;
    private Handles startPoint, endPoint;
    private float gap, angle;
    private bool autoUpdate;
    ConnectViaDuplication myTarget;
    int startAmount=5;
    private List<GameObject> objs;

    void Start() {
        myTarget=(ConnectViaDuplication)target;
        createPool();
        Debug.Log("STARTED");
    }

    void createPool() {


        objs=new List<GameObject>();

        foreach (Transform child in myTarget.transform) {
            if (child.name.Equals("PooledObject")) {
                objs.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        Debug.Log(objs.Count+" "+startAmount+" "+objs.Count);

        while (objs.Count<startAmount) {
            objs.Add((GameObject)GameObject.Instantiate(myTarget.connector));
            if (myTarget.connector.collider!=null) {
                objs [objs.Count-1].collider.enabled=false;
                objs [objs.Count-1].transform.parent=myTarget.transform;
                objs [objs.Count-1].collider.enabled=true;
            }
            else {
                objs [objs.Count-1].transform.parent=myTarget.transform;
            }
            objs [objs.Count-1].SetActive(false);
            objs [objs.Count-1].name="PooledObject";
        }
        if (objs.Count>startAmount) {

            List<GameObject> l=objs.GetRange(startAmount, objs.Count-startAmount);
            Debug.Log(l.Count);
            for (int i=0; i<l.Count; i++) {
                DestroyImmediate(l [i]);
            }

            l.Clear();


            objs.RemoveRange(startAmount, objs.Count-startAmount);
            Debug.Log(l.Count);
        }

        refresh();
    }

    void OnSceneGUI() {


        myTarget=(ConnectViaDuplication)target;
        if (myTarget.startPoint==Vector3.zero&&myTarget.endPoint==Vector3.zero) {
            myTarget.startPoint=myTarget.transform.position;
            myTarget.endPoint=myTarget.transform.position;
            myTarget.startPoint=Handles.PositionHandle(myTarget.transform.position, Quaternion.identity);
            myTarget.endPoint=Handles.PositionHandle(myTarget.transform.position, Quaternion.identity);
        }
        if (myTarget.connector!=null) {



            Handles.color=Color.blue;
            myTarget.startPoint=Handles.PositionHandle(myTarget.startPoint, Quaternion.identity);
            Handles.Disc(Quaternion.identity, myTarget.startPoint, new Vector3(0, 0, 1), 1, false, 1);
            Handles.Label(myTarget.startPoint, "Start Point");
            myTarget.endPoint=Handles.PositionHandle(myTarget.endPoint, Quaternion.identity);
            Handles.Disc(Quaternion.identity, myTarget.endPoint, new Vector3(0, 0, 1), 1, false, 1);
            Handles.Label(myTarget.endPoint, "EndPoint");

            angle=Mathf.Atan(Mathf.Abs((myTarget.endPoint.y-myTarget.startPoint.y)/(myTarget.endPoint.x-myTarget.startPoint.x)));

            if (myTarget.endPoint.x>myTarget.startPoint.x) {
                if (myTarget.endPoint.y>myTarget.startPoint.y) {
                    //quad 1
                }
                else {
                    //quad 4
                    angle=(Mathf.PI)-angle+(Mathf.PI);
                }
            }
            else {
                if (myTarget.endPoint.y>myTarget.startPoint.y) {
                    //quad 2
                    angle=(Mathf.PI/2)-angle+(Mathf.PI/2);
                }
                else {
                    //quad 3
                    angle=Mathf.PI+angle;

                }
            }

        }


    }

    public override void OnInspectorGUI() {
        myTarget=(ConnectViaDuplication)target;

        bool changed=false;
        if (oldAngle!=angle) {
            changed=true;
            oldAngle=angle;
        }
        ConnectViaDuplication.mode old=myTarget.modeChoice;
        myTarget.modeChoice=(ConnectViaDuplication.mode)EditorGUILayout.EnumPopup("Mode of creation: ", myTarget.modeChoice);
        if (myTarget.modeChoice!=old) {
            changed=true;
        }
        if (myTarget.modeChoice==ConnectViaDuplication.mode.chooseSpacing) {
            myTarget.fillChoice=(ConnectViaDuplication.fillOption)EditorGUILayout.EnumPopup("Mode of creation: ", myTarget.fillChoice);
        }

        int oldNum=myTarget.number;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        myTarget.connector=(GameObject)EditorGUILayout.ObjectField("Connector", myTarget.connector, typeof(GameObject), true);
        float spacing=myTarget.spacing;
        myTarget.spacing=Mathf.Abs(EditorGUILayout.FloatField("Spacing: ", myTarget.spacing));
        if (spacing!=myTarget.spacing) {
            changed=true;
        }
        if (myTarget.modeChoice==ConnectViaDuplication.mode.chooseNumber) {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            myTarget.number=EditorGUILayout.IntField("Number of objects: ", myTarget.number);
            if (myTarget.number>10) {
                myTarget.number=EditorGUILayout.IntSlider("Number of objects: ", myTarget.number, myTarget.number-10, myTarget.number+10);
            }
            else {
                myTarget.number=EditorGUILayout.IntSlider("Number of objects: ", myTarget.number, 0, myTarget.number+10);
            }
        }
        if (oldNum!=myTarget.number) {
            createPool();
            changed=true;
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        myTarget.delChildren=EditorGUILayout.Toggle("Delete Children: ", myTarget.delChildren);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        myTarget.hingeJointSetup=EditorGUILayout.Toggle("Set up hinge joints: ", myTarget.hingeJointSetup);
        if (myTarget.hingeJointSetup) {
            myTarget.collideWithConnected=EditorGUILayout.Toggle("Collide with hinge: ", myTarget.collideWithConnected);
            if (myTarget.connector.GetComponent<DistanceJoint2D>()) {
                myTarget.jointDist=EditorGUILayout.FloatField("Distance: ", myTarget.jointDist);
                if (myTarget.jointDist<0) {
                    myTarget.jointDist=0;
                }
            }
            if (myTarget.connector.GetComponent<SpringJoint2D>()) {
                myTarget.jointDist=EditorGUILayout.FloatField("Distance: ", myTarget.jointDist);
                myTarget.dampingRatio=EditorGUILayout.FloatField("Damping: ", myTarget.dampingRatio);
                myTarget.frequency=EditorGUILayout.FloatField("Frequency: ", myTarget.frequency);
                if (myTarget.dampingRatio>1) {
                    myTarget.dampingRatio=1;
                }
                if (myTarget.dampingRatio<0) {
                    myTarget.dampingRatio=0;
                }
                if (myTarget.jointDist<0) {
                    myTarget.jointDist=0;
                }
                if(myTarget.frequency<0){
                    myTarget.frequency=0;
                }
            }
            if (GUILayout.Button("Refresh hinges")) {
                setupHinges();
            }
        }
        myTarget.fixedEnds=EditorGUILayout.Toggle("Fix ends: ", myTarget.fixedEnds);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        myTarget.startPoint=EditorGUILayout.Vector2Field("Start Point:", myTarget.startPoint);
        myTarget.endPoint=EditorGUILayout.Vector2Field("End Point: ", myTarget.endPoint);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        autoUpdate=EditorGUILayout.Toggle("Automatically refresh: ", autoUpdate);
        if (autoUpdate) {
            if (changed)
                refresh();
        }
        else {
            if (GUILayout.Button("Refresh")) {
                refresh();
                createPool();
            }



        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Angle: "+angle*180/Mathf.PI);
        if (myTarget.modeChoice==ConnectViaDuplication.mode.chooseSpacing) {
            EditorGUILayout.LabelField("Number: "+myTarget.number);
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        startAmount=EditorGUILayout.IntField("Objects in Pool ", startAmount);
        if (GUILayout.Button("Update Pool")) {

            createPool();
        }

    }
    void numberChosenSetup() {

    }
    void refresh() {
        try {

            clearLog();




            //List<GameObject> children = new List<GameObject> ();
            //	foreach (Transform child in myTarget.gameObject.transform)
            //		children.Add (child.gameObject);
            //children.ForEach (child => DestroyImmediate (child));
            float x=myTarget.startPoint.x, y=myTarget.startPoint.y;
            Vector3 newPos, oldPos;
            oldPos=myTarget.startPoint;
            float startGap=0;
            if (myTarget.modeChoice==ConnectViaDuplication.mode.chooseNumber) {


                if (myTarget.number>1) {
                    gap=Vector3.Distance(myTarget.startPoint, myTarget.endPoint);

                    gap/=myTarget.number;
                    gap-=(myTarget.spacing);
                    if (myTarget.spacing!=0) {
                        startGap=myTarget.spacing;
                        //float startGap=0;

                        //gap+= (myTarget.connector.renderer.bounds.extents.x/2);
                        //myTarget.spacing = myTarget.spacing + gap;

                    }
                }
                else {
                    gap=0;

                }

            }
            else {
                float dist=Vector3.Distance(myTarget.startPoint, myTarget.endPoint);

                gap=myTarget.spacing+myTarget.connector.GetComponent<BoxCollider2D>().size.x;
                //	gap=Mathf.Cos (angle)*Mathf.Cos (angle);
                //	gap+=(Mathf.Sin (angle)*Mathf.Sin (angle));
                //		gap=Mathf.Pow(gap,0.5f);
                //			gap*=myTarget.spacing;




                dist/=gap;
                if (dist!=myTarget.number) {
                    removeExcess();
                }
                myTarget.number=(int)dist;
                startGap=0;

                switch (myTarget.fillChoice) {
                    case ConnectViaDuplication.fillOption.noGaps:
                        startGap=Vector3.Distance(myTarget.startPoint, myTarget.endPoint)%(gap*myTarget.number);
                        break;
                    default:
                        float makeUp=Vector3.Distance(myTarget.startPoint, myTarget.endPoint)-(gap*myTarget.number);
                        break;

                }
                //myTarget.number--;



            }





            oldPos.x=x;
            oldPos.y=y;

            if (myTarget.modeChoice!=ConnectViaDuplication.mode.chooseNumber) {
                float differenceDist=Vector3.Distance(myTarget.startPoint, myTarget.endPoint)-(gap*myTarget.number);
                switch (myTarget.fillChoice) {
                    case ConnectViaDuplication.fillOption.increaseSizeToFill:
                        if (differenceDist!=0) {
                            try {
                                //	gap*=newScale;

                                if (myTarget.number>1) {
                                    gap=Vector3.Distance(myTarget.startPoint, myTarget.endPoint);

                                    gap/=myTarget.number;
                                    gap-=(myTarget.spacing);
                                    if (myTarget.spacing!=0) {
                                        startGap=myTarget.spacing;
                                        Debug.Log(startGap);
                                        if (myTarget.endPoint.x>myTarget.startPoint.x) {
                                            x=oldPos.x+((Mathf.Cos(angle)*startGap/2));
                                        }
                                        else {
                                            x=oldPos.x-((Mathf.Cos(angle)*startGap/2));
                                        }
                                        if (myTarget.endPoint.y>myTarget.startPoint.y) {
                                            y=oldPos.y+((Mathf.Sin(angle)*startGap/2));
                                        }
                                        else {
                                            y=oldPos.y-((Mathf.Sin(angle)*startGap/2));
                                        }


                                    }
                                }
                                else {
                                    gap=0;

                                }
                            }
                            catch { }//divides evenly
                        }
                        break;
                    case ConnectViaDuplication.fillOption.spaceEvenly:
                        gap*=(1+differenceDist/Vector3.Distance(myTarget.startPoint, myTarget.endPoint));
                        break;
                    case ConnectViaDuplication.fillOption.addExtra:
                        if (differenceDist!=0) {
                            try {
                                myTarget.number+=1;
                                //	gap*=newScale;

                                if (myTarget.number>1) {
                                    gap=Vector3.Distance(myTarget.startPoint, myTarget.endPoint);

                                    gap/=myTarget.number;
                                    gap-=(myTarget.spacing);
                                    if (myTarget.spacing!=0) {
                                        startGap=myTarget.spacing;
                                        Debug.Log(startGap);
                                        if (myTarget.endPoint.x>myTarget.startPoint.x) {
                                            x=oldPos.x+((Mathf.Cos(angle)*startGap/2));
                                        }
                                        else {
                                            x=oldPos.x-((Mathf.Cos(angle)*startGap/2));
                                        }
                                        if (myTarget.endPoint.y>myTarget.startPoint.y) {
                                            y=oldPos.y+((Mathf.Sin(angle)*startGap/2));
                                        }
                                        else {
                                            y=oldPos.y-((Mathf.Sin(angle)*startGap/2));
                                        }


                                    }
                                }
                                else {
                                    gap=0;

                                }
                            }
                            catch { }//divides evenly
                        }
                        break;
                }
                Debug.Log(gap);
            }
            if (myTarget.endPoint.x>myTarget.startPoint.x) {
                x=oldPos.x+((Mathf.Cos(angle)*startGap/2));
            }
            else {
                x=oldPos.x-((Mathf.Cos(angle)*startGap/2));
            }
            if (myTarget.endPoint.y>myTarget.startPoint.y) {
                y=oldPos.y+((Mathf.Sin(angle)*startGap/2));
            }
            else {
                y=oldPos.y-((Mathf.Sin(angle)*startGap/2));
            }
            if (myTarget.number>startAmount) {
                startAmount=myTarget.number;
                createPool();
            }
            for (int i=1; i<=myTarget.number; i++) {
                //trig to calculate the transform of the next gameObject
                GameObject g;
                try {
                    g=objs [i-1];
                }
                catch {
                    Debug.Log("Recreating the pool");
                    createPool();
                    g=objs [i-1];
                }
                g.SetActive(true);
                if (g.collider!=null) {
                    g.collider.enabled=false;
                    g.transform.parent=myTarget.transform;
                    g.collider.enabled=true;
                }
                else {
                    g.transform.parent=myTarget.transform;
                }


                if (myTarget.endPoint.x>myTarget.startPoint.x) {
                    x=oldPos.x+(Mathf.Abs(Mathf.Cos(angle)*gap));
                }
                else {
                    x=oldPos.x-(Mathf.Abs(Mathf.Cos(angle)*gap));
                }
                if (myTarget.endPoint.y>myTarget.startPoint.y) {
                    y=oldPos.y+(Mathf.Abs(Mathf.Sin(angle)*gap));
                }
                else {
                    y=oldPos.y-(Mathf.Abs(Mathf.Sin(angle)*gap));
                }



                newPos=new Vector2(x, y);
                connect(g.transform, oldPos, newPos);
                oldPos=newPos;

                if (myTarget.endPoint.x>myTarget.startPoint.x) {
                    oldPos.x+=(Mathf.Abs(Mathf.Cos(angle)*myTarget.spacing));
                }
                else {

                    oldPos.x-=(Mathf.Abs(Mathf.Cos(angle)*myTarget.spacing));
                }
                if (myTarget.endPoint.y>myTarget.startPoint.y) {
                    oldPos.y+=(Mathf.Abs(Mathf.Sin(angle)*myTarget.spacing));
                }
                else {
                    oldPos.y-=(Mathf.Abs(Mathf.Sin(angle)*myTarget.spacing));
                }

            }


            if (myTarget.hingeJointSetup) {
                setupHinges();
            }
            if (myTarget.fixedEnds) {
                objs [0].rigidbody2D.isKinematic=true;
                objs [objs.Count-1].rigidbody2D.isKinematic=true;
            }
            else {
                objs [0].rigidbody2D.isKinematic=false;
                objs [objs.Count-1].rigidbody2D.isKinematic=false;
            }
        }
        catch (Exception e) {
            Debug.Log(e);
        }

    }
    void removeExcess() {
        objs=new List<GameObject>();
        foreach (Transform child in myTarget.transform) {
            if (child.name.Equals("PooledObject")) {
                objs.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        while (objs.Count<startAmount) {
            objs.Add((GameObject)GameObject.Instantiate(myTarget.connector));
            if (myTarget.connector.collider!=null) {
                objs [objs.Count-1].collider.enabled=false;
                objs [objs.Count-1].transform.parent=myTarget.transform;
                objs [objs.Count-1].collider.enabled=true;
            }
            else {
                objs [objs.Count-1].transform.parent=myTarget.transform;
            }
            objs [objs.Count-1].SetActive(false);
            objs [objs.Count-1].name="PooledObject";
        }
        if (objs.Count>startAmount) {

            List<GameObject> l=objs.GetRange(startAmount, objs.Count-startAmount);
            Debug.Log(l.Count);
            for (int i=0; i<l.Count; i++) {
                DestroyImmediate(l [i]);
            }
            l.Clear();
            objs.RemoveRange(startAmount, objs.Count-startAmount);
        }


    }
    void setupHinges() {
        try {
            Debug.Log("DONE");
            bool setDist=false;
            if (myTarget.connector.GetComponent<DistanceJoint2D>()) {
                setDist=true;
            }
            if (!setDist) {
                if (myTarget.connector.GetComponent<SpringJoint2D>()) {
                    for (int i=0; i<objs.Count-1; i++) {
                        SpringJoint2D h=objs [i].GetComponent<SpringJoint2D>();
                        h.connectedBody=(objs [i+1].rigidbody2D);
                        if (myTarget.collideWithConnected) {
                            h.collideConnected=true;
                        }
                        else {
                            h.collideConnected=false;
                        }
                        h.distance=myTarget.jointDist;
                        h.dampingRatio=myTarget.dampingRatio;
                        h.frequency=myTarget.frequency;
                    }
                }
                else {
                    for (int i=0; i<objs.Count-1; i++) {
                        AnchoredJoint2D h=objs [i].GetComponent<AnchoredJoint2D>();
                        h.connectedBody=(objs [i+1].rigidbody2D);
                        if (myTarget.collideWithConnected) {
                            h.collideConnected=true;
                        }
                        else {
                            h.collideConnected=false;
                        }
                    }
                }
            }
            else {
                for (int i=0; i<objs.Count-1; i++) {
                    DistanceJoint2D h=objs [i].GetComponent<DistanceJoint2D>();
                    h.connectedBody=(objs [i+1].rigidbody2D);
                    if (myTarget.collideWithConnected) {
                        h.collideConnected=true;
                    }
                    else {
                        h.collideConnected=false;
                    }
                    h.distance=myTarget.jointDist;
                }
            }
            if (myTarget.fixedEnds) {
                objs [0].rigidbody2D.isKinematic=true;
                objs [objs.Count-1].rigidbody2D.isKinematic=true;
            }
            objs [objs.Count-1].GetComponent<AnchoredJoint2D>().enabled=false;
        }catch{//this means that objs is blank
            refresh();
            setupHinges();

        }
    }

    void clearLog() {
        Assembly assembly=Assembly.GetAssembly(typeof(SceneView));
        Type type=assembly.GetType("UnityEditorInternal.LogEntries");
        MethodInfo method=type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    void connect(Transform obj, Vector3 start, Vector3 end) {
        //this is where the math starts
        //setting the position to halfway between the beginning and the end
        obj.position=Vector3.Lerp(start, end, 0.5f);
        //getting the scale
        if (myTarget.modeChoice==ConnectViaDuplication.mode.chooseNumber||myTarget.fillChoice==ConnectViaDuplication.fillOption.addExtra||myTarget.fillChoice==ConnectViaDuplication.fillOption.increaseSizeToFill) {
            float scale=Vector3.Distance(start, end);
            obj.transform.localScale=new Vector3(scale, obj.transform.localScale.y);
            if (scale<0) {
                Debug.LogWarning("The spacing is too high!");
            }
        }
        //Debug.Log(obj.lossyScale.x);
        //trig
        //float degrees = Mathf.Atan ((end.y - start.y) / (end.x - start.x));



        obj.rotation=Quaternion.Euler(0, 0, angle*180/Mathf.PI);

    }

}
