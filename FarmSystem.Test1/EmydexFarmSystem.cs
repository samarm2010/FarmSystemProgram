using FarmSystem.Test2;
using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace FarmSystem.Test1
{
    public delegate void FarmEmptyDelegate();  // delegate
    public class EmydexFarmSystem
    {
        Queue animalQueue = new Queue();
        public event FarmEmptyDelegate FarmEmpty; // event

        //TEST 1
        public void Enter(Animal animal)
        {
            //TODO Modify the code so that we can display the type of animal (cow, sheep etc) 
            //Hold all the animals so it is available for future activities

            animalQueue.Enqueue(animal);
            Console.WriteLine(string.Format("{0} has entered the farm", animal.GetType().Name));      
        }
     
        //TEST 2
        public void MakeNoise()
        {
            Type objectType = null;
            object objectInstance = null;

            //Test 2 : Modify this method to make the animals talk
            foreach (var item in animalQueue)
            {
                // get the object type
                objectType = item.GetType();

                // create instance of class
                objectInstance = Activator.CreateInstance(objectType);

                // invoke method using System.Reflection
                MethodInfo toInvoke = objectType.GetMethod("Talk");
                toInvoke.Invoke(objectInstance, null);
            }

            if (animalQueue.Count == 0)
                Console.WriteLine("There are no animals in the farm");
        }

        //TEST 3
        public void MilkAnimals()
        {
            Type objectType = null;
            object objectInstance = null;

            foreach (var item in animalQueue)
            {
                objectType = item.GetType();

                // create instance of class
                objectInstance = Activator.CreateInstance(objectType);
                
                // invoke method using System.Reflection
                MethodInfo toInvoke = objectType.GetMethod("ProduceMilk");

                if (toInvoke != null)
                    toInvoke.Invoke(objectInstance, null);
            }
            if (animalQueue.Count == 0)
                Console.WriteLine("Cannot identify the farm animals which can be milked");
        }

        //TEST 4
        public void ReleaseAllAnimals()
        {
            foreach (var item in animalQueue)
            {
                Console.WriteLine(string.Format("{0} has left the farm", item.GetType().Name));
            }

            animalQueue.Clear();
            
            // invoke the event
            FarmEmpty?.Invoke();            
        }
        
    }
}