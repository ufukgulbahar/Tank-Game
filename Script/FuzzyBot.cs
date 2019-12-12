using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AForge.Fuzzy;
using System;

public class FuzzyBot : MonoBehaviour
{


    public Transform player;

    float speed, distance;
    FuzzySet fsNear, fsMed, fsFar;
    FuzzySet fsSlow, fsMedium, fsFast;
    LinguisticVariable linDistance, linSpeed;

    Database database;
    InferenceSystem inf;

    // Start is called before the first frame update
    void Start()
    {
        Initializate();
    }

    private void Initializate()
    {
        DistanceFuzzySets();
        SpeedFuzzySets();
        AddToDatabase();
    }



    private void DistanceFuzzySets()
    {
        fsNear = new FuzzySet("Near", new TrapezoidalFunction(0, 10, TrapezoidalFunction.EdgeType.Right));
        fsMed = new FuzzySet("Med", new TrapezoidalFunction(0, 10, 15, 20));
        fsFar = new FuzzySet("Far", new TrapezoidalFunction(20, 30, TrapezoidalFunction.EdgeType.Left));

        linDistance = new LinguisticVariable("Distance", 0, 100);
        linDistance.AddLabel(fsNear);
        linDistance.AddLabel(fsMed);
        linDistance.AddLabel(fsFar);

    }

    private void SpeedFuzzySets()
    {
        fsSlow = new FuzzySet("Slow", new TrapezoidalFunction(5, 7, TrapezoidalFunction.EdgeType.Right));
        fsMedium = new FuzzySet("Medium", new TrapezoidalFunction(5, 7, 10, 15));
        fsFast = new FuzzySet("Fast", new TrapezoidalFunction(15, 20, TrapezoidalFunction.EdgeType.Left));

        linSpeed = new LinguisticVariable("Speed", 0, 120);
        linSpeed.AddLabel(fsSlow);
        linSpeed.AddLabel(fsMedium);
        linSpeed.AddLabel(fsFast);
    }


    private void AddToDatabase()
    {
        database = new Database();
        database.AddVariable(linDistance);
        database.AddVariable(linSpeed);


        inf = new InferenceSystem(database, new CentroidDefuzzifier(120));
        inf.NewRule("Rule 1", "IF Distance IS Near THEN Speed IS Slow");
        inf.NewRule("Rule 2", "IF Distance IS Med THEN Speed IS Medium");
        inf.NewRule("Rule 3", "IF Distance IS Far THEN Speed IS Fast");
    }





    // Update is called once per frame
    void Update()
    {
        Evaluate();
    }


    public float Evaluate()
    {
        distance = Vector3.Distance(player.position, transform.position);
        inf.SetInput("Distance", distance);
        speed = inf.Evaluate("Speed");

        Debug.Log(speed);
        return speed;
    }
}