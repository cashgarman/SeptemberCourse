using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Animal
{
    public string name;
    public int age;

    public virtual void MakeNoise()
    {
    }
}

public abstract class Quadriped : Animal, IHerbivore
{

}

public class Cat : Quadriped
{
    public override void MakeNoise()
    {
        base.MakeNoise();

        Debug.Log($"{name} meows!");
    }
}

public class Dog : Quadriped
{
    public override void MakeNoise()
    {
        base.MakeNoise();

        Debug.Log($"{name} barks!");
    }
}

public class Zoo
{
    public List<Animal> animals = new List<Animal>();

    public void SimulateZoo()
    {
        // Create some animals
        var iggy = new Cat();
        iggy.name = "Iggy";

        var thor = new Dog();
        thor.name = "Thor";

        animals.Add(iggy);
        animals.Add(thor);

        foreach (var animal in animals)
        {
            animal.MakeNoise();
        }
    }
}